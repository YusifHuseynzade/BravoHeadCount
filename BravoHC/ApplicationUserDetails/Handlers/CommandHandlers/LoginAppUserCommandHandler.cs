using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using Common.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.ApplicationUserDetails.Commands
{
    public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommandRequest, LoginAppUserCommandResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginAppUserCommandHandler(IApplicationDbContext context,
                                          IConfiguration configuration,
                                          IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı e-posta ile veritabanından getir ve rollerini de dahil et
            var appUser = await _context.AppUsers
                                        .Include(u => u.AppUserRoles)
                                        .ThenInclude(ur => ur.Role)
                                        .FirstOrDefaultAsync(u => u.Email == request.Email);

            // Kullanıcı kontrolü ve aktif olup olmadığını denetle
            if (appUser == null)
            {
                return new LoginAppUserCommandResponse { IsSuccess = false, Message = "Bu e-posta ile bir hesap bulunamadı." };
            }

            if (appUser.Password == request.Password) // Şifre zaten hashlenmiş olarak kabul ediliyor
            {
                if (!appUser.IsActive)
                {
                    appUser.IsActive = true; // Eğer kullanıcı aktif değilse aktif hale getir
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // JWT Token oluştur
                var jwtToken = GenerateJwtToken(appUser);

                // Refresh Token oluştur
                string refreshToken = await SetRefreshToken(appUser, cancellationToken);

                return new LoginAppUserCommandResponse
                {
                    IsSuccess = true,
                    UserId = appUser.Id,
                    PhoneNumber = appUser.PhoneNumber,
                    FullName = appUser.FullName,
                    Email = appUser.Email,
                    RoleIds = appUser.AppUserRoles != null ? appUser.AppUserRoles.Select(ur => ur.RoleId).ToList() : new List<int>(),
                    JwtToken = jwtToken,
                    RefreshToken = refreshToken,
                    Message = "Login successful"
                };
            }

            return new LoginAppUserCommandResponse { IsSuccess = false, Message = "Invalid credentials" };
        }

        private string GenerateJwtToken(AppUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

            string fullName = appUser.FullName;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.Email),
                 new Claim("FullName", fullName)
            };

            // Kullanıcının rollerini ekle
            if (appUser.AppUserRoles != null && appUser.AppUserRoles.Any())
            {
                foreach (var userRole in appUser.AppUserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
                }
            }

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var jwtToken = new JwtSecurityToken
            (
                    claims: claims,
                    expires: DateTime.Now.AddYears(1),
                    signingCredentials: creds
            );
            var jwt = tokenHandler.WriteToken(jwtToken);
            return jwt;
        }

        private async Task<string> SetRefreshToken(AppUser appUser, CancellationToken cancellationToken)
        {
            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            DateTime refreshTokenExpireTime = DateTime.Now.AddDays(365).ToUniversalTime();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshTokenExpireTime
            };
            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken, cookieOptions);

            appUser.RefreshToken = refreshToken;
            //appUser.RefreshTokenCreated = DateTime.Now.ToUniversalTime();
            //appUser.RefreshTokenExpires = refreshTokenExpireTime;

            await _context.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }
    }
}

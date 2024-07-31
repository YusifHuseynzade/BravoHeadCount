using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Request;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;
using Common.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Handlers.CommandHandlers
{
    public class OtpConfirmationForLoginCommandHandler : IRequestHandler<OtpConfirmationForLoginCommandRequest, OtpConfirmationForLoginCommandResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserRepository _repository;
        private readonly IApplicationDbContext _context;

        public OtpConfirmationForLoginCommandHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IApplicationDbContext context, IAppUserRepository repository)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _repository = repository;
        }

        public async Task<OtpConfirmationForLoginCommandResponse> Handle(OtpConfirmationForLoginCommandRequest request, CancellationToken cancellationToken)
        {
            // Telefon numarasına göre kullanıcıyı bul
            AppUser user = await _repository.FirstOrDefaultAsync(p => p.OTPToken == request.OtpToken, "Role");

            if (user != null)
            {
                if (user.OTPToken == request.OtpToken)
                {
                    if (user.OTPTokenExpires >= DateTime.UtcNow)
                    {
                        if (!user.IsActive)
                        {
                            user.IsActive = true;
                            await _repository.CommitAsync();
                        }

                        var jwtToken = GenerateJwtToken(user);
                        string refreshToken = await SetRefreshToken(user, cancellationToken);

                        return new OtpConfirmationForLoginCommandResponse
                        {
                            IsSuccess = true,
                            Message = "OTP confirmation successful, redirecting",
                            JwtToken = jwtToken,
                            RefreshToken = refreshToken,
                            UserId = user.Id,
                            PhoneNumber = user.PhoneNumber,
                        };
                    }
                    else
                    {
                        return new OtpConfirmationForLoginCommandResponse
                        {
                            IsSuccess = false,
                            Message = "OTP expired"
                        };
                    }
                }
            }

            return new OtpConfirmationForLoginCommandResponse
            {
                IsSuccess = false,
                Message = "OTP confirmation failed"
            };
        }

        private string GenerateJwtToken(AppUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.Email),
                new Claim(ClaimTypes.Role, appUser.Role.RoleName.ToString())
            };

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

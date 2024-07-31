using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using Common.Interfaces;
using Core.Helpers;
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
        private readonly ISmsService _smsService;

        public LoginAppUserCommandHandler(IApplicationDbContext context,
                                          IConfiguration configuration,
                                          ISmsService smsService)
                                          
        {
            _context = context;
            _configuration = configuration;
            _smsService = smsService;
        }

        public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            //Hash the password provided in the login request
           var hashedPassword = request.Password;


            //Retrieve the user from the database based on the provided username
           var appUser = await _context.AppUsers.Include(m => m.Role).FirstOrDefaultAsync(u => u.Email == request.Email);

            //Check credentials and if the user is Active
            if (appUser == null)
            {
                return new LoginAppUserCommandResponse { IsSuccess = false, Message = "Account with this email doesn't exist" };
            }
            
            if (appUser.Password == hashedPassword)
            {
                string otpToken = (RandomGenerator.NextInt() % 10000).ToString("0000");
                appUser.OTPToken = otpToken;
                appUser.OTPTokenCreated = DateTime.Now.ToUniversalTime();
                appUser.OTPTokenExpires = DateTime.Now.AddMinutes(5).ToUniversalTime();

                //SMS servisini kullanarak OTP kodunu kullanıcıya gönder
                await _smsService.SendOtpCode(appUser.PhoneNumber, otpToken);

                await _context.SaveChangesAsync(cancellationToken);

                return new LoginAppUserCommandResponse
                {
                    IsSuccess = true,
                    UserId = appUser.Id,
                    PhoneNumber = appUser.PhoneNumber,
                    FullName = appUser.FullName,
                    Email = appUser.Email,
                    RoleId = appUser.RoleId,
                };
            }
            return new LoginAppUserCommandResponse { IsSuccess = false };
        }
       
    }
}

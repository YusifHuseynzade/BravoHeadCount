using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System.Net.Mail;
using System.Net;
using Core.Helpers;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Request;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;

namespace ApplicationUserDetails.ForgotPasswordDetails.Handlers.CommandHandlers
{
    public class OtpConfirmationCommandHandler : IRequestHandler<OtpConfirmationCommandRequest, OtpConfirmationCommandResponse>
    {
        private readonly IAppUserRepository _repository;

        public OtpConfirmationCommandHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OtpConfirmationCommandResponse> Handle(OtpConfirmationCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _repository.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

            if (user != null)
            {
                if (user.OTPToken == request.OtpToken)
                {
                    if (user.OTPTokenExpires >= DateTime.UtcNow)
                    {
                        return new OtpConfirmationCommandResponse
                        {
                            IsSuccess = true,
                            Message = "OTP confirmation successfull, redirecting"
                        };
                    }
                    else
                    {
                        return new OtpConfirmationCommandResponse
                        {
                            IsSuccess = false,
                            Message = "OTP expired"
                        };
                    }
                }
            }

            return new OtpConfirmationCommandResponse
            {
                IsSuccess = false,
                Message = "OTP confirmation failed"
            };
        }

    }
}
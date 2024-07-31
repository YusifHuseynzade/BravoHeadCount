using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Core.Helpers;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Request;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;

namespace ApplicationUserDetails.ForgotPasswordDetails.Handlers.CommandHandlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
    {
        private readonly IAppUserRepository _repository;

        public ResetPasswordCommandHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _repository.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

            ValidationChecker.ValidateUserPassword(request.NewPassword);

            if (user != null)
            {
                if (request.NewPassword != request.RepeatNewPassword)
                {
                    return new ResetPasswordCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Passwords doesn't match"
                    };
                }
                else
                {
                    user.Password = request.NewPassword;

                    await _repository.UpdateAsync(user);
                    await _repository.CommitAsync();

                    return new ResetPasswordCommandResponse
                    {
                        IsSuccess = true,
                        Message = "Password reset successful"
                    };
                }
            }

            return new ResetPasswordCommandResponse
            {
                IsSuccess = false,
                Message = "Password reset failed"
            };
        }

    }
}
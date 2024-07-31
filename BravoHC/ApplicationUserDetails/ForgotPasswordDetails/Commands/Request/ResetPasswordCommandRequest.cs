using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.ForgotPasswordDetails.Commands.Request
{
    public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
    {
        public string PhoneNumber { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPassword { get; set; }
    }
}
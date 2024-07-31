using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.ForgotPasswordDetails.Commands.Request
{
    public class OtpConfirmationCommandRequest : IRequest<OtpConfirmationCommandResponse>
    {
        public string PhoneNumber { get; set; }
        public string OtpToken { get; set; }
    }
}
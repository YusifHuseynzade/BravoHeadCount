using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.ForgotPasswordDetails.Commands.Request
{
    public class ForgotPasswordCommandRequest : IRequest<ForgotPasswordCommandResponse>
    {
        public string PhoneNumber { get; set; }
    }
}
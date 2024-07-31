using ApplicationUserDetails.Commands.Response;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Commands.Request
{
    public class OtpConfirmationForLoginCommandRequest : IRequest<OtpConfirmationForLoginCommandResponse>
    {
        public string OtpToken { get; set; }
    }
}

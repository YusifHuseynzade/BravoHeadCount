using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Commands.Response
{
    public class OtpConfirmationForLoginCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
    }
}

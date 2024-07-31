using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface ISmsService
    {
        Task SendOtpCode(string toPhoneNumber, string otpToken);
        Task SendGeneratedPassword(string toPhoneNumber, string username, string password);
    }
}

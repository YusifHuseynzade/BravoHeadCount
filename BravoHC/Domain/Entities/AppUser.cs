using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUser: BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public string? OTPToken { get; set; }
        public DateTime OTPTokenCreated { get; set; }
        public DateTime OTPTokenExpires { get; set; }
        public bool IsActive { get; set; } = false;
        public int RoleId { get; set; }
        public Role Role { get; set; }
       

        public void SetCreateUserDetails(string userName, string fullName, string phoneNumber, string password, string email)
        {
            UserName = userName;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Password = password;
            Email = email;
        }

        public void SetUpdateUserDetails(string userName, string fullName, string phoneNumber, string email)
        {
            UserName = userName;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void SetActivated(bool isActive)
        {
            IsActive = isActive;
        }
    }
}

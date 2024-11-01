﻿namespace ApplicationUserDetails.Commands.Response
{
    public class LoginAppUserCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<int> RoleIds { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
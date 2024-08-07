﻿using ApplicationUserDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationUserDetails.Commands.Request
{
    public class UpdateAppUserCommandRequest : IRequest<UpdateAppUserCommandResponse>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}

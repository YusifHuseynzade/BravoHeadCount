using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Commands.Request
{
    public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
    {
        public string RoleName { get; set; }

    }
}

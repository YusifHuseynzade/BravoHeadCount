using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Commands.Request;

public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
{
    public int Id { get; set; }
    public string RoleName { get; set; }

}

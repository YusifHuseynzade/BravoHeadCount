using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Commands.Request;

public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
{
    public int Id { get; set; }
}

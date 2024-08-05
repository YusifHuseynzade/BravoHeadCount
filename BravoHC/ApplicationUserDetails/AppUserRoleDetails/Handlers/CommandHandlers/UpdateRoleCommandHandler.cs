using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
{

    private readonly IRoleRepository _repository;

    public UpdateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetAsync(x => x.Id == request.Id);

        if (role != null)
        {
            role.SetDetail(request.RoleName);


            await _repository.UpdateAsync(role);

            return new UpdateRoleCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateRoleCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}

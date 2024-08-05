using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
{
    private readonly IRoleRepository _repository;

    public DeleteRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetAsync(x => x.Id == request.Id);

        if (role == null)
        {
            return new DeleteRoleCommandResponse { IsSuccess = false };
        }

        _repository.Remove(role);
        await _repository.CommitAsync();

        return new DeleteRoleCommandResponse
        {
            IsSuccess = true
        };
    }
}

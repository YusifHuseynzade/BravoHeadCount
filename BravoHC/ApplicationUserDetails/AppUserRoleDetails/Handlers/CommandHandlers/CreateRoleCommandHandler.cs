using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{

    private readonly IRoleRepository _repository;

    public CreateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.RoleName == request.RoleName))
        {
            return new CreateRoleCommandResponse
            {
                IsSuccess = false,
            };
        }
        var role = new Role();
        role.SetDetail(request.RoleName);

        await _repository.AddAsync(role);
        await _repository.CommitAsync();
        return new CreateRoleCommandResponse
        {
            IsSuccess = true,
        };
    }
}

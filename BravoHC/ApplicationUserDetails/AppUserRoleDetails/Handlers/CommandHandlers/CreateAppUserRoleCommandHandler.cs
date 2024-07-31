using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers
{
    public class CreateAppUserRoleCommandHandler : IRequestHandler<CreateAppUserRoleCommandRequest, CreateAppUserRoleCommandResponse>
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public CreateAppUserRoleCommandHandler(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateAppUserRoleCommandResponse> Handle(CreateAppUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var appuserRole = _mapper.Map<Role>(request);

            await _repository.AddAsync(appuserRole);
            await _repository.CommitAsync();

            return new CreateAppUserRoleCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
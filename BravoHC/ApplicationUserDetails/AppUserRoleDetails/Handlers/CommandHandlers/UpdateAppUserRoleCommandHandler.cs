using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using AutoMapper;
using Domain.IRepositories;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers
{
    public class UpdateAppUserRoleCommandHandler : IRequestHandler<UpdateAppUserRoleCommandRequest, UpdateAppUserRoleCommandResponse>
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAppUserRoleCommandHandler(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateAppUserRoleCommandResponse> Handle(UpdateAppUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var appuserRole = await _repository.GetAsync(x => x.Id == request.Id);
            _mapper.Map(request, appuserRole);
            await _repository.UpdateAsync(appuserRole);

            return new UpdateAppUserRoleCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
using ApplicationUserDetails.AppUserRoleDetails.Queries.Request;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using RoleDetails.Queries.Response;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.QueriesHandlers;

public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQueryRequest, GetByIdRoleQueryResponse>
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdRoleQueryHandler(IRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdRoleQueryResponse> Handle(GetByIdRoleQueryRequest request, CancellationToken cancellationToken)
    {
        var roles = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (roles != null)
        {
            var response = _mapper.Map<GetByIdRoleQueryResponse>(roles);
            return response;
        }

        return new GetByIdRoleQueryResponse();

    }
}
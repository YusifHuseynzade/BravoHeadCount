using ApplicationUserDetails.AppUserRoleDetails.Queries.Request;
using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using RoleDetails.Queries.Response;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.QueriesHandlers;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQueryRequest, List<GetRoleListResponse>>
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public GetAllRoleQueryHandler(IRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetRoleListResponse>> Handle(GetAllRoleQueryRequest request, CancellationToken cancellationToken)
    {
        var roles = _repository.GetAll(x => true);

        var response = _mapper.Map<List<GetAllRoleQueryResponse>>(roles);

        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        var totalCount = roles.Count();

        PaginationListDto<GetAllRoleQueryResponse> model =
               new PaginationListDto<GetAllRoleQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

        return new List<GetRoleListResponse>
        {
           new GetRoleListResponse
           {
              TotalRoleCount = totalCount,
              Roles = model.Items
           }
        };

    }
}

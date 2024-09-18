using AutoMapper;
using BakuTargetDetails.Queries.Request;
using BakuTargetDetails.Queries.Response;
using Common.Constants;
using Domain.IRepositories;
using MediatR;

namespace BakuTargetDetails.Handlers.QueryHandlers
{
    public class GetBakuTargetEmployeesQueryHandler : IRequestHandler<GetBakuTargetEmployeesQueryRequest, List<GetBakuTargetEmployeeListResponse>>
    {
        private readonly IBakuTargetRepository _repository;
        private readonly IMapper _mapper;

        public GetBakuTargetEmployeesQueryHandler(IBakuTargetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBakuTargetEmployeeListResponse>> Handle(GetBakuTargetEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var bakuTarget = await _repository.FirstOrDefaultAsync(x => x.Id == request.BakuTargetId, "Employees");

            if (bakuTarget == null)
            {
                return null;
            }

            var employees = bakuTarget.Employees;
            var employeeResponse = _mapper.Map<List<GetBakuTargetEmployeesQueryResponse>>(employees);

            if (request.ShowMore != null)
            {
                employeeResponse = employeeResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = employees.Count();

            PaginationListDto<GetBakuTargetEmployeesQueryResponse> model =
                   new PaginationListDto<GetBakuTargetEmployeesQueryResponse>(employeeResponse, request.Page, request.ShowMore?.Take ?? employeeResponse.Count, totalCount);

            return new List<GetBakuTargetEmployeeListResponse>
            {
                new GetBakuTargetEmployeeListResponse
                {
                    TotalBakuTargetEmployeeCount = totalCount,
                    BakuTargetEmployees = model.Items
                }
            };
        }
    }
}

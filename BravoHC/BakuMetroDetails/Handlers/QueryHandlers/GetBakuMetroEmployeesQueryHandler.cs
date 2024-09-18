using AutoMapper;
using BakuMetroDetails.Queries.Request;
using BakuMetroDetails.Queries.Response;
using Common.Constants;
using Domain.IRepositories;
using MediatR;

namespace BakuMetroDetails.Handlers.QueryHandlers
{
    public class GetBakuMetroEmployeesQueryHandler : IRequestHandler<GetBakuMetroEmployeesQueryRequest, List<GetBakuMetroEmployeeListResponse>>
    {
        private readonly IBakuMetroRepository _repository;
        private readonly IMapper _mapper;

        public GetBakuMetroEmployeesQueryHandler(IBakuMetroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBakuMetroEmployeeListResponse>> Handle(GetBakuMetroEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var bakuMetro = await _repository.FirstOrDefaultAsync(x => x.Id == request.BakuMetroId, "Employees");

            if (bakuMetro == null)
            {
                return null;
            }

            var employees = bakuMetro.Employees;
            var employeeResponse = _mapper.Map<List<GetBakuMetroEmployeesQueryResponse>>(employees);

            if (request.ShowMore != null)
            {
                employeeResponse = employeeResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = employees.Count();

            PaginationListDto<GetBakuMetroEmployeesQueryResponse> model =
                   new PaginationListDto<GetBakuMetroEmployeesQueryResponse>(employeeResponse, request.Page, request.ShowMore?.Take ?? employeeResponse.Count, totalCount);

            return new List<GetBakuMetroEmployeeListResponse>
            {
                new GetBakuMetroEmployeeListResponse
                {
                    TotalBakuMetroEmployeeCount = totalCount,
                    BakuMetroEmployees = model.Items
                }
            };
        }
    }
}

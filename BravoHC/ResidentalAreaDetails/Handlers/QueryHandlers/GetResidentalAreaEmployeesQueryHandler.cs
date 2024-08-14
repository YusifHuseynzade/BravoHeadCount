using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ResidentalAreaDetails.Queries.Request;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Handlers.QueryHandlers
{
    public class GetResidentalAreaEmployeesQueryHandler : IRequestHandler<GetResidentalAreaEmployeesQueryRequest, List<GetResidentalAreaEmployeeListResponse>>
    {
        private readonly IResidentalAreaRepository _repository;
        private readonly IMapper _mapper;

        public GetResidentalAreaEmployeesQueryHandler(IResidentalAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetResidentalAreaEmployeeListResponse>> Handle(GetResidentalAreaEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var residentalArea = await _repository.FirstOrDefaultAsync(x => x.Id == request.ResidentalAreaId, "Employees");

            if (residentalArea == null)
            {
                return null;
            }

            var employees = residentalArea.Employees;
            var employeeResponse = _mapper.Map<List<GetResidentalAreaEmployeesQueryResponse>>(employees);

            if (request.ShowMore != null)
            {
                employeeResponse = employeeResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = employees.Count();

            PaginationListDto<GetResidentalAreaEmployeesQueryResponse> model =
                   new PaginationListDto<GetResidentalAreaEmployeesQueryResponse>(employeeResponse, request.Page, request.ShowMore?.Take ?? employeeResponse.Count, totalCount);

            return new List<GetResidentalAreaEmployeeListResponse>
            {
                new GetResidentalAreaEmployeeListResponse
                {
                    TotalResidentalAreaEmployeeCount = totalCount,
                    ResidentalAreaEmployees = model.Items
                }
            };
        }
    }
}

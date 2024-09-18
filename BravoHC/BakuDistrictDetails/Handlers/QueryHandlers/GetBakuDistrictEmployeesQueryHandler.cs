using AutoMapper;
using BakuDistrictDetails.Queries.Request;
using BakuDistrictDetails.Queries.Response;
using Common.Constants;
using Domain.IRepositories;
using MediatR;

namespace BakuDistrictDetails.Handlers.QueryHandlers
{
    public class GetBakuDistrictEmployeesQueryHandler : IRequestHandler<GetBakuDistrictEmployeesQueryRequest, List<GetBakuDistrictEmployeeListResponse>>
    {
        private readonly IBakuDistrictRepository _repository;
        private readonly IMapper _mapper;

        public GetBakuDistrictEmployeesQueryHandler(IBakuDistrictRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBakuDistrictEmployeeListResponse>> Handle(GetBakuDistrictEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var bakuDistrict = await _repository.FirstOrDefaultAsync(x => x.Id == request.BakuDistrictId, "Employees");

            if (bakuDistrict == null)
            {
                return null;
            }

            var employees = bakuDistrict.Employees;
            var employeeResponse = _mapper.Map<List<GetBakuDistrictEmployeesQueryResponse>>(employees);

            if (request.ShowMore != null)
            {
                employeeResponse = employeeResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = employees.Count();

            PaginationListDto<GetBakuDistrictEmployeesQueryResponse> model =
                   new PaginationListDto<GetBakuDistrictEmployeesQueryResponse>(employeeResponse, request.Page, request.ShowMore?.Take ?? employeeResponse.Count, totalCount);

            return new List<GetBakuDistrictEmployeeListResponse>
            {
                new GetBakuDistrictEmployeeListResponse
                {
                    TotalBakuDistrictEmployeeCount = totalCount,
                    BakuDistrictEmployees = model.Items
                }
            };
        }
    }
}

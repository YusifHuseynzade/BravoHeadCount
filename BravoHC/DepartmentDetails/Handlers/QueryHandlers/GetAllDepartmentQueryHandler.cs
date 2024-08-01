using AutoMapper;
using Common.Constants;
using DepartmentDetails.Queries.Request;
using DepartmentDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace DepartmentDetails.Queries.GetAll;


public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQueryRequest, List<GetDepartmentListResponse>>
{
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;

    public GetAllDepartmentQueryHandler(IDepartmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetDepartmentListResponse>> Handle(GetAllDepartmentQueryRequest request, CancellationToken cancellationToken)
    {
        var Departments = _repository.GetAll(x => true, "FunctionalArea");

        var response = _mapper.Map<List<GetAllDepartmentQueryResponse>>(Departments);

        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        var totalCount = Departments.Count();

        PaginationListDto<GetAllDepartmentQueryResponse> model =
               new PaginationListDto<GetAllDepartmentQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

        return new List<GetDepartmentListResponse>
        {
           new GetDepartmentListResponse
           {
              TotalDepartmentCount = totalCount,
              Departments = model.Items
           }
        };
    }

}

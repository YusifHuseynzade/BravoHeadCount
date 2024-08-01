using AutoMapper;
using Common.Constants;
using DepartmentDetails.Queries.Request;
using DepartmentDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace DepartmentDetails.Handlers.QueryHandlers;

public class GetDepartmentProjectQueryHandler : IRequestHandler<GetDepartmentProjectQueryRequest, List<GetDepartmentProjectQueryResponse>>
{
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;

    public GetDepartmentProjectQueryHandler(IDepartmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetDepartmentProjectQueryResponse>> Handle(GetDepartmentProjectQueryRequest request, CancellationToken cancellationToken)
    {
        var department = await _repository.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, "Sections");

        if (department == null)
        {

            return null;
        }

        var sections = department.Sections;
        var sectionResponse = _mapper.Map<List<GetDepartmentProjectQueryResponse>>(sections);

        if (request.ShowMore != null)
        {
            sectionResponse = sectionResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        PaginationListDto<GetDepartmentProjectQueryResponse> model =
               new PaginationListDto<GetDepartmentProjectQueryResponse>(sectionResponse, request.Page, request.ShowMore?.Take ?? sectionResponse.Count, sectionResponse.Count());

        return model.Items;
    }
}

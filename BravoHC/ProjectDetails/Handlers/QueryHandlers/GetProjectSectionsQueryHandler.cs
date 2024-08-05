using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Handlers.QueryHandlers;

public class GetProjectSectionsQueryHandler : IRequestHandler<GetProjectSectionQueryRequest, List<GetProjectSectionQueryResponse>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectSectionsQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetProjectSectionQueryResponse>> Handle(GetProjectSectionQueryRequest request, CancellationToken cancellationToken)
    {
        var department = await _repository.FirstOrDefaultAsync(x => x.Id == request.ProjectId, "Sections");

        if (department == null)
        {

            return null;
        }

        var sections = department.Sections;
        var sectionResponse = _mapper.Map<List<GetProjectSectionQueryResponse>>(sections);

        if (request.ShowMore != null)
        {
            sectionResponse = sectionResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        PaginationListDto<GetProjectSectionQueryResponse> model =
               new PaginationListDto<GetProjectSectionQueryResponse>(sectionResponse, request.Page, request.ShowMore?.Take ?? sectionResponse.Count, sectionResponse.Count());

        return model.Items;
    }
}

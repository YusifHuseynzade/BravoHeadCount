using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using FunctionalAreaDetails.Queries.Request;
using FunctionalAreaDetails.Queries.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.QueryHandlers;

public class GetFunctionalAreaProjectsQueryHandler : IRequestHandler<GetFunctionalAreaProjectsQueryRequest, List<GetFunctionalAreaProjectsQueryResponse>>
{
    private readonly IFunctionalAreaRepository _repository;
    private readonly IMapper _mapper;

    public GetFunctionalAreaProjectsQueryHandler(IFunctionalAreaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetFunctionalAreaProjectsQueryResponse>> Handle(GetFunctionalAreaProjectsQueryRequest request, CancellationToken cancellationToken)
    {
        var functionalArea = await _repository.FirstOrDefaultAsync(x => x.Id == request.FunctionalAreaId, "Projects");

        if (functionalArea == null)
        {

            return null;
        }

        var projects = functionalArea.Projects;
        var projectresponse = _mapper.Map<List<GetFunctionalAreaProjectsQueryResponse>>(projects);

        if (request.ShowMore != null)
        {
            projectresponse = projectresponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        PaginationListDto<GetFunctionalAreaProjectsQueryResponse> model =
               new PaginationListDto<GetFunctionalAreaProjectsQueryResponse>(projectresponse, request.Page, request.ShowMore?.Take ?? projectresponse.Count, projectresponse.Count());

        return model.Items;
    }
}


using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Handlers.QueryHandlers;

public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQueryRequest, List<GetAllProjectQueryResponse>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProjectQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllProjectQueryResponse>> Handle(GetAllProjectQueryRequest request, CancellationToken cancellationToken)
    {
        var projects = _repository.GetAll(x => true);

        var response = _mapper.Map<List<GetAllProjectQueryResponse>>(projects);
        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }
        PaginationListDto<GetAllProjectQueryResponse> model =
               new PaginationListDto<GetAllProjectQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, projects.Count());

        return model.Items;

    }

}

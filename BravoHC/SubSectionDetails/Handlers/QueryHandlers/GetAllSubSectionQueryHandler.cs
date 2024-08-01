using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using SubSectionDetails.Queries.Request;
using SubSectionDetails.Queries.Response;

namespace SubSectionDetails.Handlers.QueryHandlers;

public class GetAllSubSectionQueryHandler : IRequestHandler<GetAllSubSectionQueryRequest, List<GetSubSectionListResponse>>
{
    private readonly ISubSectionRepository _repository;
    private readonly IMapper _mapper;

    public GetAllSubSectionQueryHandler(ISubSectionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetSubSectionListResponse>> Handle(GetAllSubSectionQueryRequest request, CancellationToken cancellationToken)
    {
        var subSections = _repository.GetAll(x => true);

        var response = _mapper.Map<List<GetAllSubSectionQueryResponse>>(subSections);


        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        var totalCount = subSections.Count();

        PaginationListDto<GetAllSubSectionQueryResponse> model =
               new PaginationListDto<GetAllSubSectionQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

        return new List<GetSubSectionListResponse>
        {
           new GetSubSectionListResponse
           {
              TotalSubSectionCount = totalCount,
              SubSections = model.Items
           }
        };

    }
}
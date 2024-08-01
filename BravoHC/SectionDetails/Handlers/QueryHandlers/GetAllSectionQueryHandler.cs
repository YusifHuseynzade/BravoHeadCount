using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using SectionDetails.Queries.Request;
using SectionDetails.Queries.Response;

namespace SectionDetails.Handlers.QueryHandlers;

public class GetAllSectionQueryHandler : IRequestHandler<GetAllSectionQueryRequest, List<GetSectionListResponse>>
{
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;

    public GetAllSectionQueryHandler(ISectionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetSectionListResponse>> Handle(GetAllSectionQueryRequest request, CancellationToken cancellationToken)
    {
        var sections = _repository.GetAll(x => true);

        var response = _mapper.Map<List<GetAllSectionQueryResponse>>(sections);

        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        var totalCount = sections.Count();

        PaginationListDto<GetAllSectionQueryResponse> model =
               new PaginationListDto<GetAllSectionQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

        return new List<GetSectionListResponse>
        {
           new GetSectionListResponse
           {
              TotalSectionCount = totalCount,
              Sections = model.Items
           }
        };

    }
}

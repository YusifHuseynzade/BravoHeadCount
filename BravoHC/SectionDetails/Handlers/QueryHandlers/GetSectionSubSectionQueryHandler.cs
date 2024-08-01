using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using SectionDetails.Queries.Request;
using SectionDetails.Queries.Response;

namespace SectionDetails.Handlers.QueryHandlers;

public class GetSectionSubSectionQueryHandler : IRequestHandler<GetSectionSubSectionQueryRequest, List<GetSectionSubSectionQueryResponse>>
{
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;

    public GetSectionSubSectionQueryHandler(ISectionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetSectionSubSectionQueryResponse>> Handle(GetSectionSubSectionQueryRequest request, CancellationToken cancellationToken)
    {
        var section = await _repository.FirstOrDefaultAsync(x => x.Id == request.SectionId, "SubSections");

        if (section == null)
        {
            return null;
        }

        var subSection = section.SubSections;
        var subSectionResponse = _mapper.Map<List<GetSectionSubSectionQueryResponse>>(section);

        if (request.ShowMore != null)
        {
            subSectionResponse = subSectionResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        PaginationListDto<GetSectionSubSectionQueryResponse> model =
               new PaginationListDto<GetSectionSubSectionQueryResponse>(subSectionResponse, request.Page, request.ShowMore?.Take ?? subSectionResponse.Count, subSectionResponse.Count());

        return model.Items;
    }
}
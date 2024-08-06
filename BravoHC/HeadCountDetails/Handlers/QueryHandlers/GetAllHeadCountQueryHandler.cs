using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using HeadCountDetails.Queries.Request;
using HeadCountDetails.Queries.Response;
using MediatR;

namespace HeadCountDetails.Handlers.QueryHandlers;

public class GetAllHeadCountQueryHandler : IRequestHandler<GetAllHeadCountQueryRequest, List<GetAllHeadCountQueryResponse>>
{
    private readonly IHeadCountRepository _repository;
    private readonly IMapper _mapper;

    public GetAllHeadCountQueryHandler(IHeadCountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllHeadCountQueryResponse>> Handle(GetAllHeadCountQueryRequest request, CancellationToken cancellationToken)
    {
        var headCounts = _repository.GetAll(x => true);

        var response = _mapper.Map<List<GetAllHeadCountQueryResponse>>(headCounts);
        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }
        PaginationListDto<GetAllHeadCountQueryResponse> model =
               new PaginationListDto<GetAllHeadCountQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, headCounts.Count());

        return model.Items;

    }

}

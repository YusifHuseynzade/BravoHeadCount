using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using PositionDetails.Queries.Request;
using PositionDetails.Queries.Response;

namespace PositionDetails.Handlers.QueryHandlers;



public class GetAllPositionQueryHandler : IRequestHandler<GetAllPositionQueryRequest, List<GetAllPositionQueryResponse>>
{
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPositionQueryHandler(IPositionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<GetAllPositionQueryResponse>> Handle(GetAllPositionQueryRequest request, CancellationToken cancellationToken)
    {
        var positions = _repository.GetAll(x => true);

        if (positions != null)
        {
            var response = _mapper.Map<List<GetAllPositionQueryResponse>>(positions);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            PaginationListDto<GetAllPositionQueryResponse> model =
                   new PaginationListDto<GetAllPositionQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, positions.Count());

            return model.Items;
        }

        return new List<GetAllPositionQueryResponse>();
    }

}




using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using PositionDetails.Queries.Request;
using PositionDetails.Queries.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PositionDetails.Handlers.QueryHandlers
{
    public class GetAllPositionQueryHandler : IRequestHandler<GetAllPositionQueryRequest, List<GetAllPositionListQueryResponse>>
    {
        private readonly IPositionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPositionQueryHandler(IPositionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPositionListQueryResponse>> Handle(GetAllPositionQueryRequest request, CancellationToken cancellationToken)
        {
            var positions = _repository.GetAll(x => true);

            if (positions != null)
            {
                var response = _mapper.Map<List<GetAllPositionQueryResponse>>(positions);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = positions.Count();

                PaginationListDto<GetAllPositionQueryResponse> model =
                       new PaginationListDto<GetAllPositionQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllPositionListQueryResponse>
                {
                    new GetAllPositionListQueryResponse
                    {
                        TotalPositionCount = totalCount,
                        Positions = model.Items
                    }
                };
            }

            return new List<GetAllPositionListQueryResponse>();
        }
    }
}

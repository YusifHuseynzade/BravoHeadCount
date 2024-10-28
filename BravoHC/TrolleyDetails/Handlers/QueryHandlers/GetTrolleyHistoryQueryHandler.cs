using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using TrolleyDetails.Queries.Request;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Handlers.QueryHandlers
{
    public class GetTrolleyHistoryQueryHandler : IRequestHandler<GetTrolleyHistoryQueryRequest, List<GetListTrolleyHistoryQueryResponse>>
    {
        private readonly ITrolleyHistoryRepository _trolleyHistoryRepository;
        private readonly IMapper _mapper;

        public GetTrolleyHistoryQueryHandler(ITrolleyHistoryRepository trolleyHistoryRepository, IMapper mapper)
        {
            _trolleyHistoryRepository = trolleyHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListTrolleyHistoryQueryResponse>> Handle(GetTrolleyHistoryQueryRequest request, CancellationToken cancellationToken)
        {

            var trolleyHistoriesQuery = _trolleyHistoryRepository.GetAll(x => x.TrolleyId == request.TrolleyId);


            var trolleyHistories = trolleyHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetTrolleyHistoryQueryResponse>>(trolleyHistories);

            // Pagination işlemi (ShowMore)
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take).ToList();
            }

            var totalCount = trolleyHistories.Count();

            // PaginationListDto kullanarak veriyi dön
            PaginationListDto<GetTrolleyHistoryQueryResponse> model =
                   new PaginationListDto<GetTrolleyHistoryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetListTrolleyHistoryQueryResponse>
            {
                new GetListTrolleyHistoryQueryResponse
                {
                    TotalTrolleyHistoryCount = totalCount,
                    TrolleyHistories = model.Items
                }
            };
        }
    }
}

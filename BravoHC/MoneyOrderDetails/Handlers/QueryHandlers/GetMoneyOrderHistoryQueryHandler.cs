using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using MoneyOrderDetails.Queries.Request;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Handlers.QueryHandlers
{
    public class GetMoneyOrderHistoryQueryHandler : IRequestHandler<GetMoneyOrderHistoryQueryRequest, List<GetListMoneyOrderHistoryQueryResponse>>
    {
        private readonly IMoneyOrderHistoryRepository _moneyOrderHistoryRepository;
        private readonly IMapper _mapper;

        public GetMoneyOrderHistoryQueryHandler(IMoneyOrderHistoryRepository moneyOrderHistoryRepository, IMapper mapper)
        {
            _moneyOrderHistoryRepository = moneyOrderHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListMoneyOrderHistoryQueryResponse>> Handle(GetMoneyOrderHistoryQueryRequest request, CancellationToken cancellationToken)
        {

            var moneyOrderHistoriesQuery = _moneyOrderHistoryRepository.GetAll(x => x.MoneyOrderId == request.MoneyOrderId);


            var moneyOrderHistories = moneyOrderHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetMoneyOrderHistoryQueryResponse>>(moneyOrderHistories);

            // Pagination işlemi (ShowMore)
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take).ToList();
            }

            var totalCount = moneyOrderHistories.Count();

            // PaginationListDto kullanarak veriyi dön
            PaginationListDto<GetMoneyOrderHistoryQueryResponse> model =
                   new PaginationListDto<GetMoneyOrderHistoryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetListMoneyOrderHistoryQueryResponse>
            {
                new GetListMoneyOrderHistoryQueryResponse
                {
                    TotalMoneyOrderHistoryCount = totalCount,
                    MoneyOrderHistories = model.Items
                }
            };
        }
    }
}

using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using MoneyOrderDetails.Queries.Request;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Handlers.QueryHandlers
{
    public class GetAllMoneyOrderQueryHandler : IRequestHandler<GetAllMoneyOrderQueryRequest, List<GetAllMoneyOrderListQueryResponse>>
    {
        private readonly IMoneyOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMoneyOrderQueryHandler(IMoneyOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllMoneyOrderListQueryResponse>> Handle(GetAllMoneyOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var MoneyOrders = _repository.GetAll(x => true);

            if (MoneyOrders != null)
            {
                var response = _mapper.Map<List<GetAllMoneyOrderQueryResponse>>(MoneyOrders);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = MoneyOrders.Count();

                PaginationListDto<GetAllMoneyOrderQueryResponse> model =
                       new PaginationListDto<GetAllMoneyOrderQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllMoneyOrderListQueryResponse>
                {
                    new GetAllMoneyOrderListQueryResponse
                    {
                        TotalMoneyOrderCount = totalCount,
                        MoneyOrders = model.Items
                    }
                };
            }

            return new List<GetAllMoneyOrderListQueryResponse>();
        }
    }
}

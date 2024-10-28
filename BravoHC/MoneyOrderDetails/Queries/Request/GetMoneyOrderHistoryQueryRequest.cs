using Common.Constants;
using MediatR;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Queries.Request
{
    public class GetMoneyOrderHistoryQueryRequest : IRequest<List<GetListMoneyOrderHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int MoneyOrderId { get; set; }
    }
}

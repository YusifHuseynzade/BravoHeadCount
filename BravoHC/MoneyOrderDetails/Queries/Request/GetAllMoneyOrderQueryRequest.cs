using Common.Constants;
using MediatR;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Queries.Request;

public class GetAllMoneyOrderQueryRequest : IRequest<List<GetAllMoneyOrderListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

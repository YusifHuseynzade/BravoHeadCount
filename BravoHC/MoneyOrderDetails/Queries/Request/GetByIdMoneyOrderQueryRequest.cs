using MediatR;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Queries.Request;

public class GetByIdMoneyOrderQueryRequest : IRequest<GetByIdMoneyOrderQueryResponse>
{
    public int Id { get; set; }
}

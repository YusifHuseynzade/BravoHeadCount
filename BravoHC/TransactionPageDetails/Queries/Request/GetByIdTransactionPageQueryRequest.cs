using MediatR;
using TransactionPageDetails.Queries.Response;

namespace TransactionPageDetails.Queries.Request;

public class GetByIdTransactionPageQueryRequest : IRequest<GetByIdTransactionPageQueryResponse>
{
    public int Id { get; set; }
}

using Common.Constants;
using MediatR;
using TransactionPageDetails.Queries.Response;

namespace TransactionPageDetails.Queries.Request;

public class GetAllTransactionPageQueryRequest : IRequest<List<GetAllTransactionPageListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

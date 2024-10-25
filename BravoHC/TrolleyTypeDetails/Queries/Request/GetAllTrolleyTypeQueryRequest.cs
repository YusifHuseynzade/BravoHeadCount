using Common.Constants;
using MediatR;
using TrolleyTypeDetails.Queries.Response;

namespace TrolleyTypeDetails.Queries.Request;

public class GetAllTrolleyTypeQueryRequest : IRequest<List<GetAllTrolleyTypeListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

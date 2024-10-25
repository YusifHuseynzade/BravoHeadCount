using Common.Constants;
using MediatR;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Queries.Request;

public class GetAllTrolleyQueryRequest : IRequest<List<GetAllTrolleyListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

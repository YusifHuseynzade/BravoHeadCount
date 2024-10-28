using Common.Constants;
using MediatR;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Queries.Request
{
    public class GetTrolleyHistoryQueryRequest : IRequest<List<GetListTrolleyHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int TrolleyId { get; set; }
    }
}

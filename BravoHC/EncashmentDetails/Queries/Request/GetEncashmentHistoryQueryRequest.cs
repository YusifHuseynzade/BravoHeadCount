using Common.Constants;
using EncashmentDetails.Queries.Response;
using MediatR;

namespace EncashmentDetails.Queries.Request
{
    public class GetEncashmentHistoryQueryRequest : IRequest<List<GetListEncashmentHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int EncashmentId { get; set; }
    }
}

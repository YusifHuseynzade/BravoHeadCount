using Common.Constants;
using MediatR;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Queries.Request;

public class GetAllScheduledDataQueryRequest : IRequest<List<GetScheduledDataListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

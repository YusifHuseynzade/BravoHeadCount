using Common.Constants;
using MediatR;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Queries.Request;

public class GetAllScheduledDataQueryRequest : IRequest<List<GetScheduledDataListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public string? SectionName { get; set; }
    public string? PositionName { get; set; }
    public string? Badge { get; set; }
    public string? FullName { get; set; }
    public DateTime? WeekDate { get; set; }
    public void NormalizeDates()
    {
        if (WeekDate.HasValue)
        {
            WeekDate = WeekDate.Value.ToUniversalTime();
        }
    }
}

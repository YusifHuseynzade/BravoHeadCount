using Common.Constants;
using MediatR;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Queries.Request;

public class GetAllScheduledDataQueryRequest : IRequest<List<GetScheduledDataListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int? SectionId { get; set; }
    public string? Search { get; set; }
    public DateTime? WeekDate { get; set; }
    public DateTime? TargetDate { get; set; }  
    public string? StartHour { get; set; }   
    public string? EndHour { get; set; }
    public void NormalizeDates()
    {
        // Sadece dolu olan nullable DateTime değerlerini UTC'ye çeviriyoruz.
        if (WeekDate.HasValue)
            WeekDate = WeekDate.Value.ToUniversalTime();

        if (TargetDate.HasValue)
            TargetDate = TargetDate.Value.ToUniversalTime();
    }
}

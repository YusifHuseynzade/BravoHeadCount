using Common.Constants;
using EndOfMonthReportDetails.Queries.Response;
using MediatR;

namespace EndOfMonthReportDetails.Queries.Request;

public class GetAllEndOfMonthReportQueryRequest : IRequest<List<GetAllEndOfMonthReportListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

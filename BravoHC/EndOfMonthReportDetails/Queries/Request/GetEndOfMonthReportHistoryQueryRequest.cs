using Common.Constants;
using EndOfMonthReportDetails.Queries.Response;
using MediatR;

namespace EndOfMonthReportDetails.Queries.Request
{
    public class GetEndOfMonthReportHistoryQueryRequest : IRequest<List<GetListEndOfMonthReportHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int EndOfMonthReportId { get; set; }
    }
}

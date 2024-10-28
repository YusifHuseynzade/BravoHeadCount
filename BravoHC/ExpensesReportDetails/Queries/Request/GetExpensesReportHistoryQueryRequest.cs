using Common.Constants;
using ExpensesReportDetails.Queries.Response;
using MediatR;

namespace ExpensesReportDetails.Queries.Request
{
    public class GetExpensesReportHistoryQueryRequest : IRequest<List<GetListExpensesReportHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int ExpensesReportId { get; set; }
    }
}

using Common.Constants;
using ExpensesReportDetails.Queries.Response;
using MediatR;

namespace ExpensesReportDetails.Queries.Request;

public class GetAllExpensesReportQueryRequest : IRequest<List<GetAllExpensesReportListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

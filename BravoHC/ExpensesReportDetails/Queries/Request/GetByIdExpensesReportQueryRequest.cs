using ExpensesReportDetails.Queries.Response;
using MediatR;

namespace ExpensesReportDetails.Queries.Request;

public class GetByIdExpensesReportQueryRequest : IRequest<GetByIdExpensesReportQueryResponse>
{
    public int Id { get; set; }
}

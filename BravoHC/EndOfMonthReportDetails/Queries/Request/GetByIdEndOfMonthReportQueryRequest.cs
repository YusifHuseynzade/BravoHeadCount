using EndOfMonthReportDetails.Queries.Response;
using MediatR;

namespace EndOfMonthReportDetails.Queries.Request;

public class GetByIdEndOfMonthReportQueryRequest : IRequest<GetByIdEndOfMonthReportQueryResponse>
{
    public int Id { get; set; }
}

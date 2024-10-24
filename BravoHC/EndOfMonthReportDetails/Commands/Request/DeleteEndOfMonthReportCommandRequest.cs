using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Commands.Request;

public class DeleteEndOfMonthReportCommandRequest : IRequest<DeleteEndOfMonthReportCommandResponse>
{
    public int Id { get; set; }
}

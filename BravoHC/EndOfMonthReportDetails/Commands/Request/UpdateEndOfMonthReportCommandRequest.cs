using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Commands.Request;

public class UpdateEndOfMonthReportCommandRequest : IRequest<UpdateEndOfMonthReportCommandResponse>
{
    public int Id { get; set; }
}


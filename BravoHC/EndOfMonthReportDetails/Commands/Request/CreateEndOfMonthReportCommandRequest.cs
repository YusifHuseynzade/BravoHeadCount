using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Commands.Request;

public class CreateEndOfMonthReportCommandRequest : IRequest<CreateEndOfMonthReportCommandResponse>
{
    public int ProjectId { get; set; }
    public string? Name { get; set; }
    public float EncashmentAmount { get; set; }
    public float DepositAmount { get; set; }
}

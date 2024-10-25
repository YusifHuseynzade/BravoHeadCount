using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Commands.Request;

public class UpdateEndOfMonthReportCommandRequest : IRequest<UpdateEndOfMonthReportCommandResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProjectId { get; set; }
    public float EncashmentAmount { get; set; }
    public float DepositAmount { get; set; }
}


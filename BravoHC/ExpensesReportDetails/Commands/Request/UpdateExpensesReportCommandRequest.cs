using ExpensesReportDetails.Commands.Response;
using MediatR;

namespace ExpensesReportDetails.Commands.Request;

public class UpdateExpensesReportCommandRequest : IRequest<UpdateExpensesReportCommandResponse>
{
    public int Id { get; set; }
}


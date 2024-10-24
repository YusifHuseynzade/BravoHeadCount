using ExpensesReportDetails.Commands.Response;
using MediatR;

namespace ExpensesReportDetails.Commands.Request;

public class DeleteExpensesReportCommandRequest : IRequest<DeleteExpensesReportCommandResponse>
{
    public int Id { get; set; }
}

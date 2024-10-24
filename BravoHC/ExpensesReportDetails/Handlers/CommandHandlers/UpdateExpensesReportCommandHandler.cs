using Domain.IRepositories;
using ExpensesReportDetails.Commands.Request;
using ExpensesReportDetails.Commands.Response;
using MediatR;

namespace ExpensesReportDetails.Handlers.CommandHandlers;

public class UpdateExpensesReportCommandHandler : IRequestHandler<UpdateExpensesReportCommandRequest, UpdateExpensesReportCommandResponse>
{
    private readonly IExpensesReportRepository _expensesReportRepository;

    public UpdateExpensesReportCommandHandler(IExpensesReportRepository expensesReportRepository)
    {
        _expensesReportRepository = expensesReportRepository;
    }

    public async Task<UpdateExpensesReportCommandResponse> Handle(UpdateExpensesReportCommandRequest request, CancellationToken cancellationToken)
    {

    }

}

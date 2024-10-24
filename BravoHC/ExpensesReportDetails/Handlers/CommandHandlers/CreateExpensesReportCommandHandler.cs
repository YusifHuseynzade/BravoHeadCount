using Domain.IRepositories;
using ExpensesReportDetails.Commands.Request;
using ExpensesReportDetails.Commands.Response;
using MediatR;

namespace ExpensesReportDetails.Handlers.CommandHandlers;


public class CreateExpensesReportCommandHandler : IRequestHandler<CreateExpensesReportCommandRequest, CreateExpensesReportCommandResponse>
{
    private readonly IExpensesReportRepository _repository;

    public CreateExpensesReportCommandHandler(IExpensesReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateExpensesReportCommandResponse> Handle(CreateExpensesReportCommandRequest request, CancellationToken cancellationToken)
    {

    }
}



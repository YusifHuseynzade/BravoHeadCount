using Domain.IRepositories;
using ExpensesReportDetails.Commands.Request;
using ExpensesReportDetails.Commands.Response;
using MediatR;

namespace ExpensesReportDetails.Handlers.CommandHandlers;

internal class DeleteExpensesReportCommandHandler : IRequestHandler<DeleteExpensesReportCommandRequest, DeleteExpensesReportCommandResponse>
{
    private readonly IExpensesReportRepository _repository;

    public DeleteExpensesReportCommandHandler(IExpensesReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteExpensesReportCommandResponse> Handle(DeleteExpensesReportCommandRequest request, CancellationToken cancellationToken)
    {
        var ExpensesReport = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (ExpensesReport == null)
        {
            return new DeleteExpensesReportCommandResponse { IsSuccess = false };
        }

        _repository.Remove(ExpensesReport);
        await _repository.CommitAsync();

        return new DeleteExpensesReportCommandResponse
        {
            IsSuccess = true
        };
    }
}

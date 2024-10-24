using Domain.IRepositories;
using EndOfMonthReportDetails.Commands.Request;
using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Handlers.CommandHandlers;

internal class DeleteEndOfMonthReportCommandHandler : IRequestHandler<DeleteEndOfMonthReportCommandRequest, DeleteEndOfMonthReportCommandResponse>
{
    private readonly IEndOfMonthReportRepository _repository;

    public DeleteEndOfMonthReportCommandHandler(IEndOfMonthReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteEndOfMonthReportCommandResponse> Handle(DeleteEndOfMonthReportCommandRequest request, CancellationToken cancellationToken)
    {
        var EndOfMonthReport = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (EndOfMonthReport == null)
        {
            return new DeleteEndOfMonthReportCommandResponse { IsSuccess = false };
        }

        _repository.Remove(EndOfMonthReport);
        await _repository.CommitAsync();

        return new DeleteEndOfMonthReportCommandResponse
        {
            IsSuccess = true
        };
    }
}

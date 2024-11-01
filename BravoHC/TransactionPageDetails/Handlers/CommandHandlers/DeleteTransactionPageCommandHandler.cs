using Domain.IRepositories;
using MediatR;
using TransactionPageDetails.Commands.Request;
using TransactionPageDetails.Commands.Response;

namespace TransactionPageDetails.Handlers.CommandHandlers;

internal class DeleteTransactionPageCommandHandler : IRequestHandler<DeleteTransactionPageCommandRequest, DeleteTransactionPageCommandResponse>
{
    private readonly ITransactionPageRepository _repository;

    public DeleteTransactionPageCommandHandler(ITransactionPageRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteTransactionPageCommandResponse> Handle(DeleteTransactionPageCommandRequest request, CancellationToken cancellationToken)
    {
        var TransactionPage = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (TransactionPage == null)
        {
            return new DeleteTransactionPageCommandResponse { IsSuccess = false };
        }

        _repository.Remove(TransactionPage);
        await _repository.CommitAsync();

        return new DeleteTransactionPageCommandResponse
        {
            IsSuccess = true
        };
    }
}

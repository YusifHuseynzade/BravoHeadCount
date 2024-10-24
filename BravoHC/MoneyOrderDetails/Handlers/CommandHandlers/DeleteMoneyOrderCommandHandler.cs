using Domain.IRepositories;
using MediatR;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Commands.Response;

namespace MoneyOrderDetails.Handlers.CommandHandlers;

internal class DeleteMoneyOrderCommandHandler : IRequestHandler<DeleteMoneyOrderCommandRequest, DeleteMoneyOrderCommandResponse>
{
    private readonly IMoneyOrderRepository _repository;

    public DeleteMoneyOrderCommandHandler(IMoneyOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteMoneyOrderCommandResponse> Handle(DeleteMoneyOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var MoneyOrder = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (MoneyOrder == null)
        {
            return new DeleteMoneyOrderCommandResponse { IsSuccess = false };
        }

        _repository.Remove(MoneyOrder);
        await _repository.CommitAsync();

        return new DeleteMoneyOrderCommandResponse
        {
            IsSuccess = true
        };
    }
}

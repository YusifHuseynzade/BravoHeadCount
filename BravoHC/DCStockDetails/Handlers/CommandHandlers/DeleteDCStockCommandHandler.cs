using DCStockDetails.Commands.Request;
using DCStockDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace DCStockDetails.Handlers.CommandHandlers;

internal class DeleteDCStockCommandHandler : IRequestHandler<DeleteDCStockCommandRequest, DeleteDCStockCommandResponse>
{
    private readonly IDCStockRepository _repository;

    public DeleteDCStockCommandHandler(IDCStockRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteDCStockCommandResponse> Handle(DeleteDCStockCommandRequest request, CancellationToken cancellationToken)
    {
        var DCStock = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (DCStock == null)
        {
            return new DeleteDCStockCommandResponse { IsSuccess = false };
        }

        _repository.Remove(DCStock);
        await _repository.CommitAsync();

        return new DeleteDCStockCommandResponse
        {
            IsSuccess = true
        };
    }
}

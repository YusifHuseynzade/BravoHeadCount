using Domain.IRepositories;
using MediatR;
using StoreStockRequestDetails.Commands.Request;
using StoreStockRequestDetails.Commands.Response;

namespace StoreStockRequestDetails.Handlers.CommandHandlers;

internal class DeleteStoreStockRequestCommandHandler : IRequestHandler<DeleteStoreStockRequestCommandRequest, DeleteStoreStockRequestCommandResponse>
{
    private readonly IStoreStockRequestRepository _repository;

    public DeleteStoreStockRequestCommandHandler(IStoreStockRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteStoreStockRequestCommandResponse> Handle(DeleteStoreStockRequestCommandRequest request, CancellationToken cancellationToken)
    {
        var StoreStockRequest = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (StoreStockRequest == null)
        {
            return new DeleteStoreStockRequestCommandResponse { IsSuccess = false };
        }

        _repository.Remove(StoreStockRequest);
        await _repository.CommitAsync();

        return new DeleteStoreStockRequestCommandResponse
        {
            IsSuccess = true
        };
    }
}

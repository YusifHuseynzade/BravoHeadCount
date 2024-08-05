using Domain.IRepositories;
using MediatR;
using StoreDetails.Commands.Request;
using StoreDetails.Commands.Response;

namespace StoreDetails.Handlers.CommandHandlers;

public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommandRequest, DeleteStoreCommandResponse>
{
    private readonly IStoreRepository _repository;

    public DeleteStoreCommandHandler(IStoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteStoreCommandResponse> Handle(DeleteStoreCommandRequest request, CancellationToken cancellationToken)
    {
        var store = await _repository.GetAsync(x => x.Id == request.Id);

        if (store == null)
        {
            return new DeleteStoreCommandResponse { IsSuccess = false };
        }

        _repository.Remove(store);
        await _repository.CommitAsync();

        return new DeleteStoreCommandResponse
        {
            IsSuccess = true
        };
    }
}

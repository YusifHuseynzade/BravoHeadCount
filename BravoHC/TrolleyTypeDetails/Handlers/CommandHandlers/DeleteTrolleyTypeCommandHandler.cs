using Domain.IRepositories;
using MediatR;
using TrolleyTypeDetails.Commands.Request;
using TrolleyTypeDetails.Commands.Response;

namespace TrolleyTypeDetails.Handlers.CommandHandlers;

internal class DeleteTrolleyTypeCommandHandler : IRequestHandler<DeleteTrolleyTypeCommandRequest, DeleteTrolleyTypeCommandResponse>
{
    private readonly ITrolleyTypeRepository _repository;

    public DeleteTrolleyTypeCommandHandler(ITrolleyTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteTrolleyTypeCommandResponse> Handle(DeleteTrolleyTypeCommandRequest request, CancellationToken cancellationToken)
    {
        var TrolleyType = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (TrolleyType == null)
        {
            return new DeleteTrolleyTypeCommandResponse { IsSuccess = false };
        }

        _repository.Remove(TrolleyType);
        await _repository.CommitAsync();

        return new DeleteTrolleyTypeCommandResponse
        {
            IsSuccess = true
        };
    }
}

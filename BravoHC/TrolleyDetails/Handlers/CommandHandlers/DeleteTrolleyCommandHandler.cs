using Domain.IRepositories;
using MediatR;
using TrolleyDetails.Commands.Request;
using TrolleyDetails.Commands.Response;

namespace TrolleyDetails.Handlers.CommandHandlers;

internal class DeleteTrolleyCommandHandler : IRequestHandler<DeleteTrolleyCommandRequest, DeleteTrolleyCommandResponse>
{
    private readonly ITrolleyRepository _repository;

    public DeleteTrolleyCommandHandler(ITrolleyRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteTrolleyCommandResponse> Handle(DeleteTrolleyCommandRequest request, CancellationToken cancellationToken)
    {
        var Trolley = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Trolley == null)
        {
            return new DeleteTrolleyCommandResponse { IsSuccess = false };
        }

        _repository.Remove(Trolley);
        await _repository.CommitAsync();

        return new DeleteTrolleyCommandResponse
        {
            IsSuccess = true
        };
    }
}

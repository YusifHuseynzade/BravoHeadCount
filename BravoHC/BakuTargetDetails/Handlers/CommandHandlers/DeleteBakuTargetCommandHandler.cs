using BakuTargetDetails.Commands.Request;
using BakuTargetDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuTargetDetails.Handlers.CommandHandlers;

public class DeleteBakuTargetCommandHandler : IRequestHandler<DeleteBakuTargetCommandRequest, DeleteBakuTargetCommandResponse>
{
    private readonly IBakuTargetRepository _repository;

    public DeleteBakuTargetCommandHandler(IBakuTargetRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteBakuTargetCommandResponse> Handle(DeleteBakuTargetCommandRequest request, CancellationToken cancellationToken)
    {
        var bakuTarget = await _repository.GetAsync(x => x.Id == request.Id);

        if (bakuTarget == null)
        {
            return new DeleteBakuTargetCommandResponse { IsSuccess = false };
        }

        _repository.Remove(bakuTarget);
        await _repository.CommitAsync();

        return new DeleteBakuTargetCommandResponse
        {
            IsSuccess = true
        };
    }
}

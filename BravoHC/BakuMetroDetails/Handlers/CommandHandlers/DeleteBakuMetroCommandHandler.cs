using BakuMetroDetails.Commands.Request;
using BakuMetroDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuMetroDetails.Handlers.CommandHandlers;

public class DeleteBakuMetroCommandHandler : IRequestHandler<DeleteBakuMetroCommandRequest, DeleteBakuMetroCommandResponse>
{
    private readonly IBakuMetroRepository _repository;

    public DeleteBakuMetroCommandHandler(IBakuMetroRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteBakuMetroCommandResponse> Handle(DeleteBakuMetroCommandRequest request, CancellationToken cancellationToken)
    {
        var bakuMetro = await _repository.GetAsync(x => x.Id == request.Id);

        if (bakuMetro == null)
        {
            return new DeleteBakuMetroCommandResponse { IsSuccess = false };
        }

        _repository.Remove(bakuMetro);
        await _repository.CommitAsync();

        return new DeleteBakuMetroCommandResponse
        {
            IsSuccess = true
        };
    }
}

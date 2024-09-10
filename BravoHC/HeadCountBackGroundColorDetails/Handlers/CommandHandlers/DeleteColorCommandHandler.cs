using Domain.IRepositories;
using HeadCountBackGroundColorDetails.Commands.Request;
using HeadCountBackGroundColorDetails.Commands.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Handlers.CommandHandlers;

internal class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommandRequest, DeleteColorCommandResponse>
{
    private readonly IHeadCountBackgroundColorRepository _repository;

    public DeleteColorCommandHandler(IHeadCountBackgroundColorRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteColorCommandResponse> Handle(DeleteColorCommandRequest request, CancellationToken cancellationToken)
    {
        var color = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (color == null)
        {
            return new DeleteColorCommandResponse { IsSuccess = false };
        }

        _repository.Remove(color);
        await _repository.CommitAsync();

        return new DeleteColorCommandResponse
        {
            IsSuccess = true
        };
    }
}

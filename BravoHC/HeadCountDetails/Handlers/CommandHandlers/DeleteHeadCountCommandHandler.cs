using Domain.IRepositories;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.Commands.Response;
using MediatR;

namespace HeadCountDetails.Handlers.CommandHandlers;

public class DeleteHeadCountCommandHandler : IRequestHandler<DeleteHeadCountCommandRequest, DeleteHeadCountCommandResponse>
{
    private readonly IHeadCountRepository _repository;

    public DeleteHeadCountCommandHandler(IHeadCountRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteHeadCountCommandResponse> Handle(DeleteHeadCountCommandRequest request, CancellationToken cancellationToken)
    {
        var headCount = await _repository.GetAsync(x => x.Id == request.Id);

        if (headCount == null)
        {
            return new DeleteHeadCountCommandResponse { IsSuccess = false };
        }

        _repository.Remove(headCount);
        await _repository.CommitAsync();

        return new DeleteHeadCountCommandResponse
        {
            IsSuccess = true
        };
    }
}

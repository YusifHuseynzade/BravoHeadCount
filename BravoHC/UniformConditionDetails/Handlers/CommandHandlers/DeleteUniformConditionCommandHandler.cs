using Domain.IRepositories;
using MediatR;
using UniformConditionDetails.Commands.Request;
using UniformConditionDetails.Commands.Response;

namespace UniformConditionDetails.Handlers.CommandHandlers;

internal class DeleteUniformConditionCommandHandler : IRequestHandler<DeleteUniformConditionCommandRequest, DeleteUniformConditionCommandResponse>
{
    private readonly IUniformConditionRepository _repository;

    public DeleteUniformConditionCommandHandler(IUniformConditionRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteUniformConditionCommandResponse> Handle(DeleteUniformConditionCommandRequest request, CancellationToken cancellationToken)
    {
        var UniformCondition = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (UniformCondition == null)
        {
            return new DeleteUniformConditionCommandResponse { IsSuccess = false };
        }

        _repository.Remove(UniformCondition);
        await _repository.CommitAsync();

        return new DeleteUniformConditionCommandResponse
        {
            IsSuccess = true
        };
    }
}

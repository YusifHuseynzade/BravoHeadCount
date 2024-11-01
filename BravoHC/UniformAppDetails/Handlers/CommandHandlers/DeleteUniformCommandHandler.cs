using Domain.IRepositories;
using MediatR;
using UniformDetails.Commands.Request;
using UniformDetails.Commands.Response;

namespace UniformDetails.Handlers.CommandHandlers;

internal class DeleteUniformCommandHandler : IRequestHandler<DeleteUniformCommandRequest, DeleteUniformCommandResponse>
{
    private readonly IUniformRepository _repository;

    public DeleteUniformCommandHandler(IUniformRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteUniformCommandResponse> Handle(DeleteUniformCommandRequest request, CancellationToken cancellationToken)
    {
        var Uniform = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Uniform == null)
        {
            return new DeleteUniformCommandResponse { IsSuccess = false };
        }

        _repository.Remove(Uniform);
        await _repository.CommitAsync();

        return new DeleteUniformCommandResponse
        {
            IsSuccess = true
        };
    }
}

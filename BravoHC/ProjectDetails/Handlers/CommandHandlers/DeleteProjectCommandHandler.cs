using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Handlers.CommandHandlers;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommandRequest, DeleteProjectCommandResponse>
{
    private readonly IProjectRepository _repository;

    public DeleteProjectCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommandRequest request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetAsync(x => x.Id == request.Id);

        if (project == null)
        {
            return new DeleteProjectCommandResponse { IsSuccess = false };
        }

        _repository.Remove(project);
        await _repository.CommitAsync();

        return new DeleteProjectCommandResponse
        {
            IsSuccess = true
        };
    }
}

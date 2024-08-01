using Domain.IRepositories;
using FunctionalAreaDetails.Commands.Request;
using FunctionalAreaDetails.Commands.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.CommandHandlers;

public class DeleteFunctionalAreaCommandHandler : IRequestHandler<DeleteFunctionalAreaCommandRequest, DeleteFunctionalAreaCommandResponse>
{
    private readonly IFunctionalAreaRepository _repository;

    public DeleteFunctionalAreaCommandHandler(IFunctionalAreaRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteFunctionalAreaCommandResponse> Handle(DeleteFunctionalAreaCommandRequest request, CancellationToken cancellationToken)
    {
        var functionalArea = await _repository.GetAsync(x => x.Id == request.Id);

        if (functionalArea == null)
        {
            return new DeleteFunctionalAreaCommandResponse { IsSuccess = false };
        }

        _repository.Remove(functionalArea);
        await _repository.CommitAsync();

        return new DeleteFunctionalAreaCommandResponse
        {
            IsSuccess = true
        };
    }
}

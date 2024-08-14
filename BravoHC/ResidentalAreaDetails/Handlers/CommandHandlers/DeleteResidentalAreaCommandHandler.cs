using Domain.IRepositories;
using MediatR;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Commands.Response;

namespace ResidentalAreaDetails.Handlers.CommandHandlers;

public class DeleteResidentalAreaCommandHandler : IRequestHandler<DeleteResidentalAreaCommandRequest, DeleteResidentalAreaCommandResponse>
{
    private readonly IResidentalAreaRepository _repository;

    public DeleteResidentalAreaCommandHandler(IResidentalAreaRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteResidentalAreaCommandResponse> Handle(DeleteResidentalAreaCommandRequest request, CancellationToken cancellationToken)
    {
        var residentalArea = await _repository.GetAsync(x => x.Id == request.Id);

        if (residentalArea == null)
        {
            return new DeleteResidentalAreaCommandResponse { IsSuccess = false };
        }

        _repository.Remove(residentalArea);
        await _repository.CommitAsync();

        return new DeleteResidentalAreaCommandResponse
        {
            IsSuccess = true
        };
    }
}

using Domain.IRepositories;
using MediatR;
using SickLeaveDetails.Commands.Request;
using SickLeaveDetails.Commands.Response;

namespace SickLeaveDetails.Handlers.CommandHandlers;

internal class DeleteSickLeaveCommandHandler : IRequestHandler<DeleteSickLeaveCommandRequest, DeleteSickLeaveCommandResponse>
{
    private readonly ISickLeaveRepository _repository;

    public DeleteSickLeaveCommandHandler(ISickLeaveRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteSickLeaveCommandResponse> Handle(DeleteSickLeaveCommandRequest request, CancellationToken cancellationToken)
    {
        var sickLeave = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (sickLeave == null)
        {
            return new DeleteSickLeaveCommandResponse { IsSuccess = false };
        }

        _repository.Remove(sickLeave);
        await _repository.CommitAsync();

        return new DeleteSickLeaveCommandResponse
        {
            IsSuccess = true
        };
    }
}

using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetailss.Commands.Response;

namespace ScheduledDataDetails.Handlers.CommandHandlers;

public class DeleteScheduledDataCommandHandler : IRequestHandler<DeleteScheduledDataCommandRequest, DeleteScheduledDataCommandResponse>
{
    private readonly IScheduledDataRepository _repository;

    public DeleteScheduledDataCommandHandler(IScheduledDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteScheduledDataCommandResponse> Handle(DeleteScheduledDataCommandRequest request, CancellationToken cancellationToken)
    {
        var scheduledData = await _repository.GetAsync(x => x.Id == request.Id);

        if (scheduledData == null)
        {
            return new DeleteScheduledDataCommandResponse { IsSuccess = false };
        }

        _repository.Remove(scheduledData);
        await _repository.CommitAsync();

        return new DeleteScheduledDataCommandResponse
        {
            IsSuccess = true
        };
    }
}

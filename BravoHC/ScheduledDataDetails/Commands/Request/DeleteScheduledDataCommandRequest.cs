using MediatR;
using ScheduledDataDetailss.Commands.Response;

namespace ScheduledDataDetails.Commands.Request;

public class DeleteScheduledDataCommandRequest : IRequest<DeleteScheduledDataCommandResponse>
{
    public int Id { get; set; }
}

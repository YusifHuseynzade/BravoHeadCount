using MediatR;
using ScheduledDataDetails.Commands.Response;

namespace ScheduledDataDetails.Commands.Request;

public class UpdateScheduledDataCommandRequest : IRequest<UpdateScheduledDataCommandResponse>
{
    public List<WeeklyUpdateDto> ScheduleUpdates { get; set; }
}

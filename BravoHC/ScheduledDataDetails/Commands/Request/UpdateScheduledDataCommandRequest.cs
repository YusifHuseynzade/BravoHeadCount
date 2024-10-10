using MediatR;
using ScheduledDataDetails.Commands.Response;

namespace ScheduledDataDetails.Commands.Request;

public class UpdateScheduledDataCommandRequest : IRequest<UpdateScheduledDataCommandResponse>
{
    public int EmployeeId { get; set; }
    public List<ScheduledDataUpdateDto> WeeklyUpdates { get; set; }
}

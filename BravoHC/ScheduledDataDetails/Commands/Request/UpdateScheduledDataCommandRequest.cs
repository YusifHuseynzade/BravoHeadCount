using MediatR;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetails.Commands.Response;

public class UpdateScheduledDataCommandRequest : IRequest<UpdateScheduledDataCommandResponse>
{
    public int EmployeeId { get; set; }
    public List<ScheduledDataUpdateDto> WeeklyUpdates { get; set; }
}
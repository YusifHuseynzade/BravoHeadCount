using MediatR;
using ScheduledDataDetails.Commands.Response;

namespace ScheduledDataDetails.Commands.Request;

public class UpdateScheduledDataCommandRequest : IRequest<UpdateScheduledDataCommandResponse>
{
    public List<EmployeeWeeklyUpdateDto> EmployeesWeeklyUpdates { get; set; }
}

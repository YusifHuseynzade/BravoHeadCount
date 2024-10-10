using MediatR;
using VacationScheduleDetails.Commands.Response;

namespace VacationScheduleDetails.Commands.Request;

public class UpdateVacationScheduleCommandRequest : IRequest<UpdateVacationScheduleCommandResponse>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}


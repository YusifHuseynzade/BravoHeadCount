using MediatR;
using SickLeaveDetails.Commands.Response;

namespace SickLeaveDetails.Commands.Request;

public class CreateSickLeaveCommandRequest : IRequest<CreateSickLeaveCommandResponse>
{
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

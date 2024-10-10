using MediatR;
using SickLeaveDetails.Commands.Response;

namespace SickLeaveDetails.Commands.Request;

public class UpdateSickLeaveCommandRequest : IRequest<UpdateSickLeaveCommandResponse>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}


using MediatR;
using VacationScheduleDetails.Commands.Response;

namespace VacationScheduleDetails.Commands.Request;

public class DeleteVacationScheduleCommandRequest : IRequest<DeleteVacationScheduleCommandResponse>
{
    public int Id { get; set; }
}

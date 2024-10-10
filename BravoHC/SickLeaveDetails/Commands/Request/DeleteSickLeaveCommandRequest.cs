using MediatR;
using SickLeaveDetails.Commands.Response;

namespace SickLeaveDetails.Commands.Request;

public class DeleteSickLeaveCommandRequest : IRequest<DeleteSickLeaveCommandResponse>
{
    public int Id { get; set; }
}

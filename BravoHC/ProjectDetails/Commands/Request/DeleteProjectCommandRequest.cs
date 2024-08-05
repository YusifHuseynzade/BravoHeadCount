using MediatR;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Commands.Request;

public class DeleteProjectCommandRequest : IRequest<DeleteProjectCommandResponse>
{
    public int Id { get; set; }
}

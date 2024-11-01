using MediatR;
using UniformDetails.Commands.Response;

namespace UniformDetails.Commands.Request;

public class DeleteUniformCommandRequest : IRequest<DeleteUniformCommandResponse>
{
    public int Id { get; set; }
}

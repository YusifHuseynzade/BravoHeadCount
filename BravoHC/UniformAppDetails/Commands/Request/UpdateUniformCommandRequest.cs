using MediatR;
using UniformDetails.Commands.Response;

namespace UniformDetails.Commands.Request;

public class UpdateUniformCommandRequest : IRequest<UpdateUniformCommandResponse>
{
    public int Id { get; set; }
}


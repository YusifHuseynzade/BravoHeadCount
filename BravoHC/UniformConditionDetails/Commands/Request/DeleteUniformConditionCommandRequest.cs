using MediatR;
using UniformConditionDetails.Commands.Response;

namespace UniformConditionDetails.Commands.Request;

public class DeleteUniformConditionCommandRequest : IRequest<DeleteUniformConditionCommandResponse>
{
    public int Id { get; set; }
}

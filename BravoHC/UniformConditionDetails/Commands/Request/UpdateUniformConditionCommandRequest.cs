using MediatR;
using UniformConditionDetails.Commands.Response;

namespace UniformConditionDetails.Commands.Request;

public class UpdateUniformConditionCommandRequest : IRequest<UpdateUniformConditionCommandResponse>
{
    public int Id { get; set; }

}


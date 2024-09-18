using BakuTargetDetails.Commands.Response;
using MediatR;

namespace BakuTargetDetails.Commands.Request;

public class CreateBakuTargetCommandRequest : IRequest<CreateBakuTargetCommandResponse>
{
    public string Name { get; set; }
}

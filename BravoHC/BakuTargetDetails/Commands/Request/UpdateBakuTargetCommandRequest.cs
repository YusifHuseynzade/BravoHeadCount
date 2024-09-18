using BakuTargetDetails.Commands.Response;
using MediatR;

namespace BakuTargetDetails.Commands.Request;

public class UpdateBakuTargetCommandRequest : IRequest<UpdateBakuTargetCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

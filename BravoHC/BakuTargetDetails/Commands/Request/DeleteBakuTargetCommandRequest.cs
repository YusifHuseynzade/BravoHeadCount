using BakuTargetDetails.Commands.Response;
using MediatR;

namespace BakuTargetDetails.Commands.Request;

public class DeleteBakuTargetCommandRequest : IRequest<DeleteBakuTargetCommandResponse>
{
    public int Id { get; set; }
}

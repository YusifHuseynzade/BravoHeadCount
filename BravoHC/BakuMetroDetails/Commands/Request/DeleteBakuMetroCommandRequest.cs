using BakuMetroDetails.Commands.Response;
using MediatR;

namespace BakuMetroDetails.Commands.Request;

public class DeleteBakuMetroCommandRequest : IRequest<DeleteBakuMetroCommandResponse>
{
    public int Id { get; set; }
}

using BakuMetroDetails.Commands.Response;
using MediatR;

namespace BakuMetroDetails.Commands.Request;

public class UpdateBakuMetroCommandRequest : IRequest<UpdateBakuMetroCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

using BakuMetroDetails.Commands.Response;
using MediatR;

namespace BakuMetroDetails.Commands.Request;

public class CreateBakuMetroCommandRequest : IRequest<CreateBakuMetroCommandResponse>
{
    public string Name { get; set; }
}

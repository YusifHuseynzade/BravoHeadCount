using HeadCountBackGroundColorDetails.Commands.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Commands.Request;

public class CreateColorCommandRequest : IRequest<CreateColorCommandResponse>
{
    public string ColorHexCode { get; set; }
}

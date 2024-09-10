using HeadCountBackGroundColorDetails.Commands.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Commands.Request;

public class UpdateColorCommandRequest : IRequest<UpdateColorCommandResponse>
{
    public int Id { get; set; }
    public string ColorHexCode { get; set; }
}


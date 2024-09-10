using HeadCountBackGroundColorDetails.Commands.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Commands.Request;

public class DeleteColorCommandRequest : IRequest<DeleteColorCommandResponse>
{
    public int Id { get; set; }
}

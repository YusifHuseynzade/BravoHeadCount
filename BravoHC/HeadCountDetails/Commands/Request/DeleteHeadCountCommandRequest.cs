using HeadCountDetails.Commands.Response;
using MediatR;

namespace HeadCountDetails.Commands.Request;

public class DeleteHeadCountCommandRequest : IRequest<DeleteHeadCountCommandResponse>
{
    public int Id { get; set; }
}

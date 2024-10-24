using EncashmentDetails.Commands.Response;
using MediatR;

namespace EncashmentDetails.Commands.Request;

public class DeleteEncashmentCommandRequest : IRequest<DeleteEncashmentCommandResponse>
{
    public int Id { get; set; }
}

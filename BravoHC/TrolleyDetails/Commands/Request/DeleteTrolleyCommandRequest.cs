using MediatR;
using TrolleyDetails.Commands.Response;

namespace TrolleyDetails.Commands.Request;

public class DeleteTrolleyCommandRequest : IRequest<DeleteTrolleyCommandResponse>
{
    public int Id { get; set; }
}

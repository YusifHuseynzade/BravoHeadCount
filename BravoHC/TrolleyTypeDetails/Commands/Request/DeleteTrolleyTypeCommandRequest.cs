using MediatR;
using TrolleyTypeDetails.Commands.Response;

namespace TrolleyTypeDetails.Commands.Request;

public class DeleteTrolleyTypeCommandRequest : IRequest<DeleteTrolleyTypeCommandResponse>
{
    public int Id { get; set; }
}

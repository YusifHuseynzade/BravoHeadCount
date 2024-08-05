using MediatR;
using StoreDetails.Commands.Response;

namespace StoreDetails.Commands.Request;

public class DeleteStoreCommandRequest : IRequest<DeleteStoreCommandResponse>
{
    public int Id { get; set; }
}

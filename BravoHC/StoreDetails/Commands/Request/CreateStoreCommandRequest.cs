using MediatR;
using StoreDetails.Commands.Response;

namespace StoreDetails.Commands.Request;

public class CreateStoreCommandRequest : IRequest<CreateStoreCommandResponse>
{
    public int ProjectId { get; set; }
    public int HeadCountNumber { get; set; }
}

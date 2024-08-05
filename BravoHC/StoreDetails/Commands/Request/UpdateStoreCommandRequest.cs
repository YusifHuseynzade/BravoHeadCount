using MediatR;
using StoreDetails.Commands.Response;

namespace StoreDetails.Commands.Request;

public class UpdateStoreCommandRequest : IRequest<UpdateStoreCommandResponse>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int FunctionalAreaId { get; set; }
    public int FormatId { get; set; }
    public int HeadCountNumber { get; set; }

}

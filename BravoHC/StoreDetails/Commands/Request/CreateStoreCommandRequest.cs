using MediatR;
using StoreDetails.Commands.Response;

namespace StoreDetails.Commands.Request;

public class CreateStoreCommandRequest : IRequest<CreateStoreCommandResponse>
{
    public int? DirectorId { get; set; }
    public int? AreaManagerId { get; set; }
    public int? StoreManagerId { get; set; }
    public int? RecruiterId { get; set; }
    public int ProjectId { get; set; }
    public int FunctionalAreaId { get; set; }
    public int FormatId { get; set; }
    public int HeadCountNumber { get; set; }
}

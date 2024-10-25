using EncashmentDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EncashmentDetails.Commands.Request;

public class CreateEncashmentCommandRequest : IRequest<CreateEncashmentCommandResponse>
{
    public string? Name { get; set; }
    public int ProjectId { get; set; }
    public float AmountFromSales { get; set; }
    public float AmountFoundOnSite { get; set; }
    public float SafeSurplus { get; set; }
    public int BranchId { get; set; }
    public string SealNumber { get; set; }
    public List<IFormFile> AttachmentFiles { get; set; } = new();
}

using EncashmentDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EncashmentDetails.Commands.Request;

public class UpdateEncashmentCommandRequest : IRequest<UpdateEncashmentCommandResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProjectId { get; set; }
    public float AmountFromSales { get; set; }
    public float AmountFoundOnSite { get; set; }
    public float SafeSurplus { get; set; }
    public string SealNumber { get; set; }
    public List<IFormFile>? NewAttachmentFiles { get; set; } = new();  // Yeni dosyalar
    public List<int>? DeleteAttachmentFileId { get; set; } = new();  // Silinecek dosya adları
}


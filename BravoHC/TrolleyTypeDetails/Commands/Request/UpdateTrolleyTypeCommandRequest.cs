using MediatR;
using Microsoft.AspNetCore.Http;
using TrolleyTypeDetails.Commands.Response;

namespace TrolleyTypeDetails.Commands.Request;

public class UpdateTrolleyTypeCommandRequest : IRequest<UpdateTrolleyTypeCommandResponse>
{
    public int Id { get; set; }  // Güncellenecek Trolley Type'ın ID'si
    public string? Name { get; set; }  // Yeni adı (opsiyonel)
    public IFormFile? NewImage { get; set; }  // Yeni görsel dosyası (opsiyonel)
}


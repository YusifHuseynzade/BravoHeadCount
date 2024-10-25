using MediatR;
using Microsoft.AspNetCore.Http;
using TrolleyTypeDetails.Commands.Response;

namespace TrolleyTypeDetails.Commands.Request;

public class CreateTrolleyTypeCommandRequest : IRequest<CreateTrolleyTypeCommandResponse>
{
    public string Name { get; set; }  // Trolley Type ismi
    public IFormFile? Image { get; set; }  // Görsel dosyası
}

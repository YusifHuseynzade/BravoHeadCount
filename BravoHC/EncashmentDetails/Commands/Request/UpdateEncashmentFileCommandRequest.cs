using EncashmentDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EncashmentDetails.Commands.Request
{
    public class UpdateEncashmentFileCommandRequest : IRequest<UpdateEncashmentFileCommandResponse>
    {
        public int Id { get; set; }
        public IFormFile AttachmentFile { get; set; }
    }
}

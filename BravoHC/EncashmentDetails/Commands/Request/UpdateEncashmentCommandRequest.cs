using EncashmentDetails.Commands.Response;
using MediatR;

namespace EncashmentDetails.Commands.Request;

public class UpdateEncashmentCommandRequest : IRequest<UpdateEncashmentCommandResponse>
{
    public int Id { get; set; }
}


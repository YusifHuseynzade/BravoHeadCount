using FormatDetails.Commands.Response;
using MediatR;

namespace FormatDetails.Commands.Request;

public class DeleteFormatCommandRequest : IRequest<DeleteFormatCommandResponse>
{
    public int Id { get; set; }
}

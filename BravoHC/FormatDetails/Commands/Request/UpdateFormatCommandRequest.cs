using FormatDetails.Commands.Response;
using MediatR;

namespace FormatDetails.Commands.Request;

public class UpdateFormatCommandRequest : IRequest<UpdateFormatCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

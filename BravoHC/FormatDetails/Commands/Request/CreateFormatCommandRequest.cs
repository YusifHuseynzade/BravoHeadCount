using FormatDetails.Commands.Response;
using MediatR;


namespace FormatDetails.Commands.Request;

public class CreateFormatCommandRequest : IRequest<CreateFormatCommandResponse>
{
    public string Name { get; set; }
}

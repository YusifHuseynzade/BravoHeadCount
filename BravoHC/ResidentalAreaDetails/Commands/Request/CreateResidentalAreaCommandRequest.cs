using MediatR;
using ResidentalAreaDetails.Commands.Response;

namespace ResidentalAreaDetails.Commands.Request;

public class CreateResidentalAreaCommandRequest : IRequest<CreateResidentalAreaCommandResponse>
{
    public string Name { get; set; }
    public string Metro { get; set; }
    public string District { get; set; }
    public string Target { get; set; }
}

using MediatR;
using ResidentalAreaDetails.Commands.Response;

namespace ResidentalAreaDetails.Commands.Request;

public class UpdateResidentalAreaCommandRequest : IRequest<UpdateResidentalAreaCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

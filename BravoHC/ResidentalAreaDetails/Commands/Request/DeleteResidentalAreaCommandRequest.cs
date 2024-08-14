using MediatR;
using ResidentalAreaDetails.Commands.Response;

namespace ResidentalAreaDetails.Commands.Request;

public class DeleteResidentalAreaCommandRequest : IRequest<DeleteResidentalAreaCommandResponse>
{
    public int Id { get; set; }
}

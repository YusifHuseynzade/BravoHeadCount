using BakuDistrictDetails.Commands.Response;
using MediatR;

namespace BakuDistrictDetails.Commands.Request;

public class DeleteBakuDistrictCommandRequest : IRequest<DeleteBakuDistrictCommandResponse>
{
    public int Id { get; set; }
}

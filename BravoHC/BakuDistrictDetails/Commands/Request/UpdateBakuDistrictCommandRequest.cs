using BakuDistrictDetails.Commands.Response;
using MediatR;

namespace BakuDistrictDetails.Commands.Request;

public class UpdateBakuDistrictCommandRequest : IRequest<UpdateBakuDistrictCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }


}

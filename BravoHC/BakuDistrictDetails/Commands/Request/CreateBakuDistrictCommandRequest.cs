using BakuDistrictDetails.Commands.Response;
using MediatR;

namespace BakuDistrictDetails.Commands.Request;

public class CreateBakuDistrictCommandRequest : IRequest<CreateBakuDistrictCommandResponse>
{
    public string Name { get; set; }

}

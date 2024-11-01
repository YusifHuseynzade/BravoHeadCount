using DCStockDetails.Commands.Response;
using MediatR;

namespace DCStockDetails.Commands.Request;

public class UpdateDCStockCommandRequest : IRequest<UpdateDCStockCommandResponse>
{
    public int Id { get; set; }
}


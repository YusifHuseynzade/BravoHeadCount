using DCStockDetails.Commands.Response;
using MediatR;

namespace DCStockDetails.Commands.Request;

public class DeleteDCStockCommandRequest : IRequest<DeleteDCStockCommandResponse>
{
    public int Id { get; set; }
}

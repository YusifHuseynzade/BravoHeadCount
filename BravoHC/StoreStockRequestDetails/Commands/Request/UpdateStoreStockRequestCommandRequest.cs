using MediatR;
using StoreStockRequestDetails.Commands.Response;

namespace StoreStockRequestDetails.Commands.Request;

public class UpdateStoreStockRequestCommandRequest : IRequest<UpdateStoreStockRequestCommandResponse>
{
    public int Id { get; set; }
}


using MediatR;
using StoreStockRequestDetails.Commands.Response;

namespace StoreStockRequestDetails.Commands.Request;

public class DeleteStoreStockRequestCommandRequest : IRequest<DeleteStoreStockRequestCommandResponse>
{
    public int Id { get; set; }
}

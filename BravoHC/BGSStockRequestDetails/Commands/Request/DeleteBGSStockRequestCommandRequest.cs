using BGSStockRequestDetails.Commands.Response;
using MediatR;

namespace BGSStockRequestDetails.Commands.Request;

public class DeleteBGSStockRequestCommandRequest : IRequest<DeleteBGSStockRequestCommandResponse>
{
    public int Id { get; set; }
}

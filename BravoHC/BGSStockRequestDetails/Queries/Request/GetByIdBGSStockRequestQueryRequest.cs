using BGSStockRequestDetails.Queries.Response;
using MediatR;

namespace BGSStockRequestDetails.Queries.Request;

public class GetByIdBGSStockRequestQueryRequest : IRequest<GetByIdBGSStockRequestQueryResponse>
{
    public int Id { get; set; }
}

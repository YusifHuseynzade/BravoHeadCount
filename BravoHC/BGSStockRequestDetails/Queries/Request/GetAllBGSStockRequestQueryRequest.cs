using BGSStockRequestDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BGSStockRequestDetails.Queries.Request;

public class GetAllBGSStockRequestQueryRequest : IRequest<List<GetAllBGSStockRequestListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

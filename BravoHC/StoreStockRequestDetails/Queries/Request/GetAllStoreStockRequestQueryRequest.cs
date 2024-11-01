using Common.Constants;
using MediatR;
using StoreStockRequestDetails.Queries.Response;

namespace StoreStockRequestDetails.Queries.Request;

public class GetAllStoreStockRequestQueryRequest : IRequest<List<GetAllStoreStockRequestListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

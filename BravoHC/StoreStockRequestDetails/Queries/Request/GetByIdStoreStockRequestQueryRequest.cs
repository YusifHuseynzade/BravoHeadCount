using MediatR;
using StoreStockRequestDetails.Queries.Response;

namespace StoreStockRequestDetails.Queries.Request;

public class GetByIdStoreStockRequestQueryRequest : IRequest<GetByIdStoreStockRequestQueryResponse>
{
    public int Id { get; set; }
}

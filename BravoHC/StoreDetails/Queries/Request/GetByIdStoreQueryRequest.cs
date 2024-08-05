using MediatR;
using StoreDetails.Queries.Response;

namespace StoreDetails.Queries.Request;

public class GetByIdStoreQueryRequest : IRequest<GetByIdStoreQueryResponse>
{
    public int Id { get; set; }
}

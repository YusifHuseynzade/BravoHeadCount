using DCStockDetails.Queries.Response;
using MediatR;

namespace DCStockDetails.Queries.Request;

public class GetByIdDCStockQueryRequest : IRequest<GetByIdDCStockQueryResponse>
{
    public int Id { get; set; }
}

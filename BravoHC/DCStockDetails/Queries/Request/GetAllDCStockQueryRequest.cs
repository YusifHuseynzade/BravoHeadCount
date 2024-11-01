using Common.Constants;
using DCStockDetails.Queries.Response;
using MediatR;

namespace DCStockDetails.Queries.Request;

public class GetAllDCStockQueryRequest : IRequest<List<GetAllDCStockListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

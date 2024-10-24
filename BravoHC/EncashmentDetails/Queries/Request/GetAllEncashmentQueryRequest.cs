using Common.Constants;
using EncashmentDetails.Queries.Response;
using MediatR;

namespace EncashmentDetails.Queries.Request;

public class GetAllEncashmentQueryRequest : IRequest<List<GetAllEncashmentListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

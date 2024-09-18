using BakuTargetDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BakuTargetDetails.Queries.Request;

public class GetAllBakuTargetQueryRequest : IRequest<List<GetBakuTargetListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

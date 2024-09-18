using BakuDistrictDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BakuDistrictDetails.Queries.Request;

public class GetAllBakuDistrictQueryRequest : IRequest<List<GetBakuDistrictListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

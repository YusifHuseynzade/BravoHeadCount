using BakuMetroDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BakuMetroDetails.Queries.Request;

public class GetAllBakuMetroQueryRequest : IRequest<List<GetResidentalAreaListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

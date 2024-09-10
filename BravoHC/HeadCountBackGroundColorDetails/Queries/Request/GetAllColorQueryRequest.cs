using Common.Constants;
using HeadCountBackGroundColorDetails.Queries.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Queries.Request;

public class GetAllColorQueryRequest : IRequest<List<GetAllColorListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

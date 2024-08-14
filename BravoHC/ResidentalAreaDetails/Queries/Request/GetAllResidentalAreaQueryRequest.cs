using Common.Constants;
using MediatR;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Queries.Request;

public class GetAllResidentalAreaQueryRequest : IRequest<List<GetResidentalAreaListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

using Common.Constants;
using MediatR;
using SubSectionDetails.Queries.Response;

namespace SubSectionDetails.Queries.Request;

public class GetAllSubSectionQueryRequest : IRequest<List<GetSubSectionListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

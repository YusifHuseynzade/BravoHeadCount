using Common.Constants;
using MediatR;
using SectionDetails.Queries.Response;

namespace SectionDetails.Queries.Request;

public class GetAllSectionQueryRequest : IRequest<List<GetSectionListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

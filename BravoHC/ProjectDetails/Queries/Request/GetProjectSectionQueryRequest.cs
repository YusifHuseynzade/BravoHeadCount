using Common.Constants;
using MediatR;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Queries.Request;

public class GetProjectSectionQueryRequest : IRequest<List<GetProjectSectionQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int ProjectId { get; set; }
}

using Common.Constants;
using MediatR;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Queries.Request;

public class GetAllProjectQueryRequest : IRequest<List<GetAllProjectQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

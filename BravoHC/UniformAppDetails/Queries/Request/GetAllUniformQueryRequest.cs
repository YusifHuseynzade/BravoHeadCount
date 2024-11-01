using Common.Constants;
using MediatR;
using UniformDetails.Queries.Response;

namespace UniformDetails.Queries.Request;

public class GetAllUniformQueryRequest : IRequest<List<GetAllUniformListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

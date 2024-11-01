using Common.Constants;
using MediatR;
using UniformConditionDetails.Queries.Response;

namespace UniformConditionDetails.Queries.Request;

public class GetAllUniformConditionQueryRequest : IRequest<List<GetAllUniformConditionListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

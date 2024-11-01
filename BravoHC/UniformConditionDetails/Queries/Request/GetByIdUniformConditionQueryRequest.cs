using MediatR;
using UniformConditionDetails.Queries.Response;

namespace UniformConditionDetails.Queries.Request;

public class GetByIdUniformConditionQueryRequest : IRequest<GetByIdUniformConditionQueryResponse>
{
    public int Id { get; set; }
}

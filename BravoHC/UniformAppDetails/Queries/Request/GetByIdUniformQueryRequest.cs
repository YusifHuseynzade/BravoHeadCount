using MediatR;
using UniformDetails.Queries.Response;

namespace UniformDetails.Queries.Request;

public class GetByIdUniformQueryRequest : IRequest<GetByIdUniformQueryResponse>
{
    public int Id { get; set; }
}

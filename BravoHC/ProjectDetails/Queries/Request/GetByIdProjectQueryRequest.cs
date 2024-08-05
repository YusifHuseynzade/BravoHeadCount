using MediatR;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Queries.Request;

public class GetByIdProjectQueryRequest : IRequest<GetByIdProjectQueryResponse>
{
    public int Id { get; set; }
}

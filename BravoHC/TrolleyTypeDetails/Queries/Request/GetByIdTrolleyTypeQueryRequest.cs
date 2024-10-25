using MediatR;
using TrolleyTypeDetails.Queries.Response;

namespace TrolleyTypeDetails.Queries.Request;

public class GetByIdTrolleyTypeQueryRequest : IRequest<GetByIdTrolleyTypeQueryResponse>
{
    public int Id { get; set; }
}

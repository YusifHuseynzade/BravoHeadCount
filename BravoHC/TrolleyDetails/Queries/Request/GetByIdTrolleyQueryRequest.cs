using MediatR;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Queries.Request;

public class GetByIdTrolleyQueryRequest : IRequest<GetByIdTrolleyQueryResponse>
{
    public int Id { get; set; }
}

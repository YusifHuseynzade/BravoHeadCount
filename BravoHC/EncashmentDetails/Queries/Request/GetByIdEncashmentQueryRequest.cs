using EncashmentDetails.Queries.Response;
using MediatR;

namespace EncashmentDetails.Queries.Request;

public class GetByIdEncashmentQueryRequest : IRequest<GetByIdEncashmentQueryResponse>
{
    public int Id { get; set; }
}

using BakuTargetDetails.Queries.Response;
using MediatR;

namespace BakuTargetDetails.Queries.Request;

public class GetByIdBakuTargetQueryRequest : IRequest<GetByIdBakuTargetQueryResponse>
{
    public int Id { get; set; }
}

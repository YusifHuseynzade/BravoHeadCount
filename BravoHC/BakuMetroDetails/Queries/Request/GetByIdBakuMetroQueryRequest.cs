using BakuMetroDetails.Queries.Response;
using MediatR;

namespace BakuMetroDetails.Queries.Request;

public class GetByIdBakuMetroQueryRequest : IRequest<GetByIdBakuMetroQueryResponse>
{
    public int Id { get; set; }
}

using HeadCountDetails.Queries.Response;
using MediatR;

namespace HeadCountDetails.Queries.Request;

public class GetByIdHeadCountQueryRequest : IRequest<GetByIdHeadCountQueryResponse>
{
    public int Id { get; set; }
}

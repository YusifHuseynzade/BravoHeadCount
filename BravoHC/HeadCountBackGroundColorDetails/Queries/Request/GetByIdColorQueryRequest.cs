using HeadCountBackGroundColorDetails.Queries.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Queries.Request;

public class GetByIdColorQueryRequest : IRequest<GetByIdColorQueryResponse>
{
    public int Id { get; set; }
}

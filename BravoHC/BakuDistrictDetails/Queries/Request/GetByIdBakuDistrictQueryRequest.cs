using BakuDistrictDetails.Queries.Response;
using MediatR;

namespace BakuDistrictDetails.Queries.Request;

public class GetByIdBakuDistrictQueryRequest : IRequest<GetByIdBakuDistrictQueryResponse>
{
    public int Id { get; set; }
}

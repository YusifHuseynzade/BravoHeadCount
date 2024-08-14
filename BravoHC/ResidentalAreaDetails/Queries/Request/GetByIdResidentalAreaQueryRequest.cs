using MediatR;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Queries.Request;

public class GetByIdResidentalAreaQueryRequest : IRequest<GetByIdResidentalAreaQueryResponse>
{
    public int Id { get; set; }
}

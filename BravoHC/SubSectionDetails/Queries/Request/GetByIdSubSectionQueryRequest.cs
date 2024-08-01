using MediatR;
using SubSectionDetails.Queries.Response;

namespace SubSectionDetails.Queries.Request;

public class GetByIdSubSectionQueryRequest : IRequest<GetByIdSubSectionQueryResponse>
{
    public int Id { get; set; }
}

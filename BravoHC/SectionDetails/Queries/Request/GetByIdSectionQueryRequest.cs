using MediatR;
using SectionDetails.Queries.Response;

namespace SectionDetails.Queries.Request;

public class GetByIdSectionQueryRequest : IRequest<GetByIdSectionQueryResponse>
{
    public int Id { get; set; }
}

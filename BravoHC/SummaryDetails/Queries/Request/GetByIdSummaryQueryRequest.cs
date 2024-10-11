using MediatR;
using SummaryDetails.Queries.Response;

namespace SummaryDetails.Queries.Request;

public class GetByIdSummaryQueryRequest : IRequest<GetByIdSummaryQueryResponse>
{
    public int Id { get; set; }
}

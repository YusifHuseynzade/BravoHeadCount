using MediatR;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Queries.Request;

public class GetByIdScheduledDataQueryRequest : IRequest<GetByIdScheduledDataQueryResponse>
{
    public int Id { get; set; }
}

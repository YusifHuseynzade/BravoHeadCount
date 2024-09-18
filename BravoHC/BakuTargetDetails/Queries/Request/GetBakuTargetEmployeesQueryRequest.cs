using BakuTargetDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BakuTargetDetails.Queries.Request;

public class GetBakuTargetEmployeesQueryRequest : IRequest<List<GetBakuTargetEmployeeListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int BakuTargetId { get; set; }
}

using Common.Constants;
using MediatR;
using SickLeaveDetails.Queries.Response;

namespace SickLeaveDetails.Queries.Request;

public class GetAllSickLeaveQueryRequest : IRequest<List<GetAllSickLeaveListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

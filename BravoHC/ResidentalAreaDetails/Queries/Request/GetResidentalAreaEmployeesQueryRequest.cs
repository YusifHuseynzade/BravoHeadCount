using Common.Constants;
using MediatR;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Queries.Request;

public class GetResidentalAreaEmployeesQueryRequest : IRequest<List<GetResidentalAreaEmployeeListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int ResidentalAreaId { get; set; }
}

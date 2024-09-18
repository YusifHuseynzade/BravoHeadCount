using BakuDistrictDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BakuDistrictDetails.Queries.Request;

public class GetBakuDistrictEmployeesQueryRequest : IRequest<List<GetBakuDistrictEmployeeListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int BakuDistrictId { get; set; }
}

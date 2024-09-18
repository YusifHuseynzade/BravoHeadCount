using BakuMetroDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace BakuMetroDetails.Queries.Request;

public class GetBakuMetroEmployeesQueryRequest : IRequest<List<GetBakuMetroEmployeeListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int BakuMetroId { get; set; }
}

using Common.Constants;
using HeadCountDetails.Queries.Response;
using MediatR;

namespace HeadCountDetails.Queries.Request;

public class GetAllHeadCountQueryRequest : IRequest<List<GetHeadCountListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public string Searchbox { get; set; }
    public int? ProjectId { get; set; }
    public List<int>?  SectionIds { get; set; }
    public int? PositionId { get; set; }
    public bool? ExcessStaff { get; set; }
    public bool? IsVacant { get; set; }
    public string? OrderBy { get; set; }
}

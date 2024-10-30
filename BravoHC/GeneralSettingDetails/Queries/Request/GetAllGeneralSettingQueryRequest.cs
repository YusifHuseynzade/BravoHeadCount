using Common.Constants;
using GeneralSettingDetails.Queries.Response;
using MediatR;

namespace GeneralSettingDetails.Queries.Request;

public class GetAllGeneralSettingQueryRequest : IRequest<List<GetAllGeneralSettingListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

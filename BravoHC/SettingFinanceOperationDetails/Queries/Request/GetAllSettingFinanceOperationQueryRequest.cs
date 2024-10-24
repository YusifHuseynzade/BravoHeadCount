using Common.Constants;
using MediatR;
using SettingFinanceOperationDetails.Queries.Response;

namespace SettingFinanceOperationDetails.Queries.Request;

public class GetAllSettingFinanceOperationQueryRequest : IRequest<List<GetAllSettingFinanceOperationListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

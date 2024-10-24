using MediatR;
using SettingFinanceOperationDetails.Queries.Response;

namespace SettingFinanceOperationDetails.Queries.Request;

public class GetByIdSettingFinanceOperationQueryRequest : IRequest<GetByIdSettingFinanceOperationQueryResponse>
{
    public int Id { get; set; }
}

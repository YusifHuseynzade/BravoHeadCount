using GeneralSettingDetails.Queries.Response;
using MediatR;

namespace GeneralSettingDetails.Queries.Request;

public class GetByIdGeneralSettingQueryRequest : IRequest<GetByIdGeneralSettingQueryResponse>
{
    public int Id { get; set; }
}

using FormatDetails.Queries.Response;
using MediatR;

namespace FormatDetails.Queries.Request;

public class GetByIdFormatQueryRequest : IRequest<GetByIdFormatQueryResponse>
{
    public int Id { get; set; }
}

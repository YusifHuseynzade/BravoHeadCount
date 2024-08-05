using Common.Constants;
using FormatDetails.Queries.Response;
using MediatR;

namespace FormatDetails.Queries.Request;

public class GetAllFormatQueryRequest : IRequest<List<GetAllFormatQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

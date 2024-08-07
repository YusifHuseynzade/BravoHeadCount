using Common.Constants;
using MediatR;
using SectionDetails.Queries.Response;

namespace SectionDetails.Queries.Request
{
    public class GetSectionSubSectionQueryRequest : IRequest<List<GetSectionSubSectionListResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int SectionId { get; set; }
    }
}

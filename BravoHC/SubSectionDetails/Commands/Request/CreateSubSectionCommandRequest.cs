using MediatR;
using SubSectionDetails.Commands.Response;

namespace SubSectionDetails.Commands.Request
{
    public class CreateSubSectionCommandRequest : IRequest<CreateSubSectionCommandResponse>
    {
        public string Name { get; set; }
        public int SectionId { get; set; }
    }
}

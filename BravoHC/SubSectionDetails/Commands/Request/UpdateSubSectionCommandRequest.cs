using MediatR;
using SubSectionDetails.Commands.Response;

namespace SubSectionDetails.Commands.Request;

public class UpdateSubSectionCommandRequest : IRequest<UpdateSubSectionCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SectionId { get; set; }
}

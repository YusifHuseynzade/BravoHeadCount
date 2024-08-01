using MediatR;
using SectionDetails.Commands.Response;

namespace SectionDetails.Commands.Request;

public class CreateSectionCommandRequest : IRequest<CreateSectionCommandResponse>
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
}

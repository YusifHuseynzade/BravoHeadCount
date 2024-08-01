using MediatR;
using SectionDetails.Commands.Response;

namespace SectionDetails.Commands.Request;

public class UpdateSectionCommandRequest : IRequest<UpdateSectionCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }


}

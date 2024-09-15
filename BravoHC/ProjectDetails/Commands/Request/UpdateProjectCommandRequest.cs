using MediatR;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Commands.Request;

public class UpdateProjectCommandRequest : IRequest<UpdateProjectCommandResponse>
{
    public int Id { get; set; }
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public bool IsStore { get; set; }
    public bool IsHeadOffice { get; set; }
    public int FunctionalAreaId { get; set; }
    public List<int> SectionIds { get; set; }
}

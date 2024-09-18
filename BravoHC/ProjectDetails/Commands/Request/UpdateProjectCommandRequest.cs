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
    public bool? IsActive { get; set; }
    public string Format { get; set; }
    public string FunctionalArea { get; set; }
    public string Director { get; set; }
    public string DirectorEmail { get; set; }
    public string AreaManager { get; set; }
    public string AreaManagerEmail { get; set; }
    public string StoreManagerEmail { get; set; }
    public string Recruiter { get; set; }
    public string RecruiterEmail { get; set; }
    public List<int> SectionIds { get; set; }
}

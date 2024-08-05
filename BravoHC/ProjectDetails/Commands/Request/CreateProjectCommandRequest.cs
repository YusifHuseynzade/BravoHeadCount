using MediatR;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Commands.Request;

public class CreateProjectCommandRequest : IRequest<CreateProjectCommandResponse>
{
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public bool IsStore { get; set; }
    public bool IsHeadOffice { get; set; }
    public int FunctionalAreaId { get; set; }
}

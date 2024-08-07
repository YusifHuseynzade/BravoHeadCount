using EmployeeDetails.Commands.Response;
using MediatR;


namespace EmployeeDetails.Commands.Request;

public class CreateEmployeeCommandRequest : IRequest<CreateEmployeeCommandResponse>
{
    public string FullName { get; set; }
    public string Badge { get; set; }
    public int FunctionalAreaId { get; set; }
    public int ProjectId { get; set; }
    public int PositionId { get; set; }
    public int SectionId { get; set; }
    public int? SubSectionId { get; set; }
}

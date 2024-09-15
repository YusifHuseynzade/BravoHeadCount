using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Queries.Response;

public class GetAllHeadCountQueryResponse
{
	public int Id { get; set; }
    public ProjectResponse Project { get; set; }
    public FunctionalAreaResponse FunctionalArea { get; set; }
    public SectionResponse Section { get; set; }
    public SubSectionResponse SubSection{ get; set; }
    public ColorResponse Color { get; set; }
    public PositionResponse Position { get; set; }
    public EmployeeResponse Employee { get; set; }
    public ManagerResponse ParentName { get; set; }
    public bool IsVacant { get; set; }
    public string RecruiterComment { get; set; }
    public int HCNumber { get; set; }
}

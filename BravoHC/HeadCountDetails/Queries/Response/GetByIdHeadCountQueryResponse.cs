using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Queries.Response;

public class GetByIdHeadCountQueryResponse
{
	public int Id { get; set; }
    public int ProjectId { get; set; }
    public int FunctionalAreaId { get; set; }
    public int SectionId { get; set; }
    public int SubSectionId { get; set; }
    public int PositionId { get; set; }
    public int EmployeeId { get; set; }
    public int ColorId { get; set; }
    public int ParentId { get; set; }
    public bool IsVacant { get; set; }
    public string RecruiterComment { get; set; }
    public int HCNumber { get; set; }
}

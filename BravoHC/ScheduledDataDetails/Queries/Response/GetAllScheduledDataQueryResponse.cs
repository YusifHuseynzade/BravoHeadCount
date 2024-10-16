using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response;

public class GetAllScheduledDataQueryResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int? PlanId { get; set; }
    public string PlanValue { get; set; }
    public string PlanColor { get; set; }
    public string PlanShift { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeBadge { get; set; }
    public string EmployeeSection { get; set; }
    public string EmployeePosition { get; set; }
    public int HolidayBalance { get; set; }
    public int VacationBalance { get; set; }
    public DateTime? Fact { get; set; }
 
}

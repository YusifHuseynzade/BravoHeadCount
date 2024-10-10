using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SickLeaveDetails.Queries.Response;

public class GetByIdSickLeaveQueryResponse
{
    public int Id { get; set; }
    public EmployeeResponse Employee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
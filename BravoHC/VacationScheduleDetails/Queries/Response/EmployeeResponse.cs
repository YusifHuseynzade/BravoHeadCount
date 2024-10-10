using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationScheduleDetails.Queries.Response
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Badge { get; set; }
        public SectionResponse Section { get; set; }
        public PositionResponse Position { get; set; }
    }
}

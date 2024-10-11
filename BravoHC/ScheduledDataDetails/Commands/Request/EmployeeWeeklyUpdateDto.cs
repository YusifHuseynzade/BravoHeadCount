using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Commands.Request
{
    public class EmployeeWeeklyUpdateDto
    {
        public int EmployeeId { get; set; }
        public List<ScheduledDataUpdateDto> WeeklyUpdates { get; set; }
    }
}

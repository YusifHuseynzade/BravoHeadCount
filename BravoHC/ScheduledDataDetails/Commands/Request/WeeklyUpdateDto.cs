using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Commands.Request
{
    public class WeeklyUpdateDto
    {
        public List<ScheduledDataUpdateDto> WeeklyUpdates { get; set; }
    }
}

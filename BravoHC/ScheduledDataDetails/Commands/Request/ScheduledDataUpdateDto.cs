using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Commands.Request
{
    public class ScheduledDataUpdateDto
    {
        public int ScheduledDataId { get; set; }
        public int PlanId { get; set; }
        public int? FactId { get; set; }
    }
}

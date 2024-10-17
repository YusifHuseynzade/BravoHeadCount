using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response
{
    public class GroupedScheduledDataDto
    {
        public Employee Employee { get; set; }
        public List<ScheduledData> ScheduledDataList { get; set; }
    }
}

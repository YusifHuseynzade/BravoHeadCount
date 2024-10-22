using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Fact:BaseEntity
    {
        public string Value { get; set; }
        public List<ScheduledData> ScheduledDatas { get; set; }
    }
}

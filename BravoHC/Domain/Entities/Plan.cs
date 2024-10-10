using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan: BaseEntity
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public string Color { get; set; } 
        public string Shift { get; set; }
        public List<ScheduledData> ScheduledDatas { get; set; }
    }
}

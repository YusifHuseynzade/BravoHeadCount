using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ScheduledData: BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(4);
        public int? PlanId { get; set; }
        public Plan? Plan { get; set; }
        public int? FactId { get; set; }
        public Fact? Fact { get; set; }
        public int? VacationScheduleId { get; set; }
        public VacationSchedule? VacationSchedule { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}

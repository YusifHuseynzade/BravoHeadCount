using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeBalance: BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int HolidayBalance { get; set; }
        public int VacationBalance { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}

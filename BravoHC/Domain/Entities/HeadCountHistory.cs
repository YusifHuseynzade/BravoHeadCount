using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HeadCountHistory : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int FromProjectId { get; set; }
        public Project FromProject { get; set; }
        public int ToProjectId { get; set; }
        public Project ToProject { get; set; }
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}

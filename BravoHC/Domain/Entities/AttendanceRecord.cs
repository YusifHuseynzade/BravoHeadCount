using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AttendanceRecord: BaseEntity
    {
        public string EmployeeId { get; set; }
        public DateTime AccessDateTime { get; set; }
        public DateTime AccessDate { get; set; }
        public string DeviceName { get; set; }
        public string PersonName { get; set; }
    }
}

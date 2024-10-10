using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Summary: BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
        public int WorkdaysCount { get; set; }
        public int VacationDaysCount { get; set; }
        public int SickDaysCount { get; set; }
        public int DayOffCount { get; set; }
        public int AbsentDaysCount { get; set; }
    }
}

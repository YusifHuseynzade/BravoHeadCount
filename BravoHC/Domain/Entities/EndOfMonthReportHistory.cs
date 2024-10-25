using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EndOfMonthReportHistory : BaseEntity
    {
        public int EndOfMonthReportId { get; set; }
        public float EncashmentAmount { get; set; }
        public float DepositAmount { get; set; }
        public float TotalAmount { get; set; }
        public string? Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}

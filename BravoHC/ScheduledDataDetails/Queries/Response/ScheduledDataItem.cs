using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response
{
    public class ScheduledDataItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // Tarih
        public int? PlanId { get; set; } // Plan ID'si
        public string PlanValue { get; set; } // Planın değeri (Örneğin, "Vardiya")
        public string PlanColor { get; set; } // Planın rengi
        public string PlanShift { get; set; } // Planın vardiya bilgisi
        public DateTime? Fact { get; set; } // Gerçekleşen tarih
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}

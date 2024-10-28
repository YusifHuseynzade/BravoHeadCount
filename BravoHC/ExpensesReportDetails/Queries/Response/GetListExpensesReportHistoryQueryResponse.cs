using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Queries.Response
{
    public class GetListExpensesReportHistoryQueryResponse
    {
        public int TotalExpensesReportHistoryCount { get; set; }
        public List<GetExpensesReportHistoryQueryResponse> ExpensesReportHistories { get; set; }
    }
}

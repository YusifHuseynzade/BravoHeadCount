using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Queries.Response
{
    public class GetAllExpensesReportListQueryResponse
    {
        public int TotalExpensesReportCount { get; set; }
        public List<GetAllExpensesReportQueryResponse> ExpensesReports { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndOfMonthReportDetails.Queries.Response
{
    public class GetAllEndOfMonthReportListQueryResponse
    {
        public int TotalEndOfMonthReportCount { get; set; }
        public List<GetAllEndOfMonthReportQueryResponse> EndOfMonthReports { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndOfMonthReportDetails.Queries.Response
{
    public class GetListEndOfMonthReportHistoryQueryResponse
    {
        public int TotalEndOfMonthReportHistoryCount { get; set; }
        public List<GetEndOfMonthReportHistoryQueryResponse> EndOfMonthReportHistories { get; set; }
    }
}

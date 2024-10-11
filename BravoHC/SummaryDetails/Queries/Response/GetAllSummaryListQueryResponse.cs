using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryDetails.Queries.Response
{
    public class GetAllSummaryListQueryResponse
    {
        public int TotalVacationScheduleCount { get; set; }
        public List<GetAllSummaryQueryResponse> VacationSchedules { get; set; }
    }
}

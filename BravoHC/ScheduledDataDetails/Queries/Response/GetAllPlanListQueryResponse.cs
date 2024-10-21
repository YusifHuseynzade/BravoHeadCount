using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response
{
    public class GetAllPlanListQueryResponse
    {
        public int TotalPlanCount { get; set; }
        public List<GetAllPlanQueryResponse> Plans { get; set; }
    }
}

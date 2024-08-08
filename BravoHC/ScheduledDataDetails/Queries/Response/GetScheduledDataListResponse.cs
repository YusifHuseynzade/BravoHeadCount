using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response
{
    public class GetScheduledDataListResponse
    {
        public int TotalScheduledDataCount { get; set; }
        public List<GetAllScheduledDataQueryResponse> ScheduledDatas { get; set; }
    }
}

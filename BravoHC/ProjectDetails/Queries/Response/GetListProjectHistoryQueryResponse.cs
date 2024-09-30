using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response
{
    public class GetListProjectHistoryQueryResponse
    {
        public int TotalProjectHistoryCount { get; set; }
        public List<GetProjectHistoryQueryResponse> ProjectHistories { get; set; }
    }
}

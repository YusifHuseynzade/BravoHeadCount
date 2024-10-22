using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response
{
    public class GetAllFactListQueryResponse
    {
        public int TotalFactCount { get; set; }
        public List<GetAllFactQueryResponse> Facts { get; set; }
    }
}

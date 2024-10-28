using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncashmentDetails.Queries.Response
{
    public class GetListEncashmentHistoryQueryResponse
    {
        public int TotalEncashmentHistoryCount { get; set; }
        public List<GetEncashmentHistoryQueryResponse> EncashmentHistories { get; set; }
    }
}

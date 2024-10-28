using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyDetails.Queries.Response
{
    public class GetListTrolleyHistoryQueryResponse
    {
        public int TotalTrolleyHistoryCount { get; set; }
        public List<GetTrolleyHistoryQueryResponse> TrolleyHistories { get; set; }
    }
}

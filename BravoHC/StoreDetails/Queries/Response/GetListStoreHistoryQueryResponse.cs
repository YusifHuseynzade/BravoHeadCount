using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDetails.Queries.Response
{
    public class GetListStoreHistoryQueryResponse
    {
        public int TotalStoreHistoryCount { get; set; }
        public List<GetStoreHistoryQueryResponse> StoreHistories { get; set; }
    }
}

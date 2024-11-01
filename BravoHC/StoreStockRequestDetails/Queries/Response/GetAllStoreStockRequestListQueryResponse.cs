using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreStockRequestDetails.Queries.Response
{
    public class GetAllStoreStockRequestListQueryResponse
    {
        public int TotalStoreStockRequestCount { get; set; }
        public List<GetAllStoreStockRequestQueryResponse> StoreStockRequests { get; set; }
    }
}

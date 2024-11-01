using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGSStockRequestDetails.Queries.Response
{
    public class GetAllBGSStockRequestListQueryResponse
    {
        public int TotalBGSStockRequestCount { get; set; }
        public List<GetAllBGSStockRequestQueryResponse> BGSStockRequests { get; set; }
    }
}

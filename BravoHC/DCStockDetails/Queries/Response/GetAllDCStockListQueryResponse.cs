using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCStockDetails.Queries.Response
{
    public class GetAllDCStockListQueryResponse
    {
        public int TotalDCStockCount { get; set; }
        public List<GetAllDCStockQueryResponse> DCStocks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDetails.Queries.Response
{
    public class GetAllStoreListQueryResponse
    {
        public int TotalStoreCount { get; set; }
        public List<GetAllStoreQueryResponse> Stores { get; set; }
    }
}

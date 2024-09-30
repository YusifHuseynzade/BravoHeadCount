using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDetails.Queries.Response
{
    public class GetStoreHistoryQueryResponse
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int OldHeadCountNumber { get; set; }
        public int NewHeadCountNumber { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

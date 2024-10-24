using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyOrderDetails.Queries.Response
{
    public class GetAllMoneyOrderListQueryResponse
    {
        public int TotalMoneyOrderCount { get; set; }
        public List<GetAllMoneyOrderQueryResponse> MoneyOrders { get; set; }
    }
}

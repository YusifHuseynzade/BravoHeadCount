using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionPageDetails.Queries.Response
{
    public class GetAllTransactionPageListQueryResponse
    {
        public int TotalTransactionCount { get; set; }
        public List<GetAllTransactionPageQueryResponse> Transactions { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyOrderDetails.Queries.Response
{
    public class GetListMoneyOrderHistoryQueryResponse
    {
        public int TotalMoneyOrderHistoryCount { get; set; }
        public List<GetMoneyOrderHistoryQueryResponse> MoneyOrderHistories { get; set; }
    }
}

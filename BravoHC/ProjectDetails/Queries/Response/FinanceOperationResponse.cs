﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response
{
    public class FinanceOperationResponse
    {
        public string? Name { get; set; }
        public string EncashmentDays { get; set; }
        public DateTime DateEncashment { get; set; }
        public bool IsActiveEncashment { get; set; }
        public int FrequencyEncashment { get; set; }
        public List<string> EncashmentRecipient { get; set; }
        public string MoneyOrderDays { get; set; }
        public DateTime DateMoneyOrder { get; set; }
        public bool IsActiveMoneyOrder { get; set; }
        public int FrequencyMoneyOrder { get; set; }
        public List<string> MoneyOrderRecipient { get; set; }
        public int BranchId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EncashmentHistory : BaseEntity
    {
        public int EncashmentId { get; set; }
        public string? Name { get; set; }
        public float AmountFromSales { get; set; }
        public float AmountFoundOnSite { get; set; }
        public float SafeSurplus { get; set; }
        public float TotalAmount { get; set; }
        public string SealNumber { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string ModifiedBy { get; set; }
    }
}

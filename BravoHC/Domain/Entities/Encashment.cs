﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Encashment : BaseEntity
    {
        public string? Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public float AmountFromSales { get; set; }
        public float AmountFoundOnSite { get; set; }
        public float SafeSurplus { get; set; }
        public float TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public string SealNumber { get; set; }
        public List<Attachment> Attachments { get; set; } = new();

        public void SetDetails(string name, float amountFromSales, float amountFoundOnSite,
                           float safeSurplus, DateTime modifiedDate, string sealNumber, string modifiedBy)
        {
            Name = name;
            AmountFromSales = amountFromSales;
            AmountFoundOnSite = amountFoundOnSite;
            SafeSurplus = safeSurplus;
            ModifiedDate = modifiedDate;
            SealNumber = sealNumber;
            ModifiedBy = modifiedBy;
        }
    }
}

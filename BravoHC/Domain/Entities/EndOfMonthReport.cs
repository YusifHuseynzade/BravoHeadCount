using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EndOfMonthReport : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public float EncashmentAmount { get; set; }
        public float DepositAmount { get; set; }
        public float PettyCashAmount { get; set; }
        public float TotalAmount { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public void SetDetails(string marketCodeAndName, float encashmentAmount, float depositAmount,
                           float pettyCashAmount, float totalAmount, string name, string createdBy)
        {
            EncashmentAmount = encashmentAmount;
            DepositAmount = depositAmount;
            PettyCashAmount = pettyCashAmount;
            TotalAmount = totalAmount;
            Name = name;
            CreatedBy = createdBy;
        }
    }
}

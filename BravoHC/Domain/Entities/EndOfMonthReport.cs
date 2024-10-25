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
        public float TotalAmount { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public void SetDetails(float encashmentAmount, float depositAmount,
                           float totalAmount, string name, string createdBy)
        {
            EncashmentAmount = encashmentAmount;
            DepositAmount = depositAmount;
            TotalAmount = totalAmount;
            Name = name;
            CreatedBy = createdBy;
        }
    }
}

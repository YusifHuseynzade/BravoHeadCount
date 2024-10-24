using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SettingFinanceOperation : BaseEntity
    {
        public string Name { get; set; }
        public string EncashmentDays { get; set; }
        public DateTime DateEncashment { get; set; }
        public bool IsActiveEncashment { get; set; }
        public int FrequencyEncashment { get; set; }
        public List<string> EncashmentRecipient { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string MoneyOrderDays { get; set; }
        public DateTime DateMoneyOrder { get; set; }
        public bool IsActiveMoneyOrder { get; set; }
        public int FrequencyMoneyOrder { get; set; }
        public List<string> MoneyOrderRecipient { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public void SetDetails(string name, string encashmentDays, DateTime dateEncashment, bool isActiveEncashment, string createdBy,
                       string moneyOrderDays,
                       DateTime dateMoneyOrder, bool isActiveMoneyOrder, int frequencyEncashment, int frequencyMoneyOrder,
                       string modifiedBy)
        {
            Name = name;
            EncashmentDays = encashmentDays;
            DateEncashment = dateEncashment;
            IsActiveEncashment = isActiveEncashment;
            MoneyOrderDays = moneyOrderDays;
            DateMoneyOrder = dateMoneyOrder;
            IsActiveMoneyOrder = isActiveMoneyOrder;
            FrequencyEncashment = frequencyEncashment;
            FrequencyMoneyOrder = frequencyMoneyOrder;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MoneyOrder : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int HundredAZN { get; set; }
        public float FiftyAZN { get; set; }
        public float TwentyAZN { get; set; }
        public float TenAZN { get; set; }
        public float FiveAZN { get; set; }
        public float OneAZN { get; set; }
        public float FiftyQapik { get; set; }
        public float TwentyQapik { get; set; }
        public float TenQapik { get; set; }
        public float FiveQapik { get; set; }
        public float ThreeQapik { get; set; }
        public float OneQapik { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalAmount { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public string SealNumber { get; set; }
        public void SetDetails(int hundredAZN, float fiftyAZN, float twentyAZN,
                          float tenAZN, float fiveAZN, float oneAZN, float fiftyQapik,
                          float twentyQapik, float tenQapik, float fiveQapik, float threeQapik,
                          float oneQapik, int totalQuantity, string name, string modifiedBy)
        {
            HundredAZN = hundredAZN;
            FiftyAZN = fiftyAZN;
            TwentyAZN = twentyAZN;
            TenAZN = tenAZN;
            FiveAZN = fiveAZN;
            OneAZN = oneAZN;
            FiftyQapik = fiftyQapik;
            TwentyQapik = twentyQapik;
            TenQapik = tenQapik;
            FiveQapik = fiveQapik;
            ThreeQapik = threeQapik;
            OneQapik = oneQapik;
            TotalQuantity = totalQuantity;
            Name = name;
            ModifiedBy = modifiedBy;
        }
    }
}

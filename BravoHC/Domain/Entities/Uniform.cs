using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Uniform: BaseEntity
    {
        public string UniCode { get; set; }
        public string UniName { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public string UniType { get; set; }
        public string? ImageUrl { get; set; }
        public int UsageDuration { get; set; }
        public List<DCStock> DCStocks { get; set; }
        public List<BGSStockRequest> BGSStockRequests { get; set; }
        public List<StoreStockRequest> StoreStockRequests { get; set; }
        public List<TransactionPage> Transactions { get; set; }
        public void InitializeUniform(string uniCode, string uniName, string gender, string size, string uniType, string imageUrl)
        {
            UniCode = uniCode;
            UniName = uniName;
            Gender = gender;
            Size = size;
            UniType = uniType;
            ImageUrl = imageUrl;
        }
    }
}

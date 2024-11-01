using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DCStock: BaseEntity
    {
        public int StockCount { get; set; } 
        public int ImportedStockCount { get; set; } 
        public string StoreOrUser { get; set; } 
        public decimal UnitPrice { get; set; } 
        public decimal TotalPrice { get; set; }
        public int UniformId { get; set; }
        public Uniform Uniform {  get; set; }
        public string OrderId { get; set; }
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public void InitializeDCStock(int stockCount, int importedStockCount, string storeOrUser, decimal unitPrice, decimal totalPrice, DateTime receptionDate, string orderId, DateTime modifiedDate, string createdBy)
        {
            StockCount = stockCount;
            ImportedStockCount = importedStockCount;
            StoreOrUser = storeOrUser;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            ReceptionDate = receptionDate;
            OrderId = orderId;
            ModifiedDate = modifiedDate;
            CreatedBy = createdBy;
        }
    }
}

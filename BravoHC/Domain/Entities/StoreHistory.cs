using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StoreHistory : BaseEntity
    {
        public int StoreId { get; set; } // Foreign Key for Store
        public Store Store { get; set; }
        public int OldHeadCountNumber { get; set; } // Eski HeadCount sayısı
        public int NewHeadCountNumber { get; set; } // Yeni HeadCount sayısı
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4); // Değişiklik tarihi
    }
}

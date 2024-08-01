using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string Badge { get; set; }
        public bool IsDirector { get; set; }
        public bool IsRecruiter { get; set; }
        public bool IsAreaManager { get; set; }
        public bool IsStoreManager { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}

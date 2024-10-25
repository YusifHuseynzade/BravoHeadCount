using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Trolley: BaseEntity
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TrolleyTypeId { get; set; }
        public TrolleyType TrolleyType { get; set; }
        public DateTime CountDate { get; set; }
        public int WorkingTrolleysCount {  get; set; }
        public int BrokenTrolleysCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

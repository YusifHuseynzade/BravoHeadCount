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
        public List<TrolleyType> TrolleyTypes { get; set; }
        public DateTime CountDate { get; set; }
        public int WorkingTrolleysCount {  get; set; }
        public int BrokenTrolleysCount { get; set; }
    }
}

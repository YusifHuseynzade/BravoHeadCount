using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Month: BaseEntity
    {
        public int Number { get; set; } 
        public string Name { get; set; }
        public List<Summary> Summaries { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department: BaseEntity
    {
        public string Name { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        public List<Section> Sections { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

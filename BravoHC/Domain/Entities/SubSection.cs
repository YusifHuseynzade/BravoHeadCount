using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SubSection : BaseEntity
    {
        public string Name { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public List<Employee> Employees { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

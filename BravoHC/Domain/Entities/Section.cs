using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Section : BaseEntity
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public List<SubSection> SubSections { get; set; }
        public List<Employee> Employees { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

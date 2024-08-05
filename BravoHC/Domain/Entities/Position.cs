using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

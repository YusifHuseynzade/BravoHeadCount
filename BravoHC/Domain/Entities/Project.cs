using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project: BaseEntity
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public bool IsStore { get; set; }
        public bool IsHeadOffice { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        public List<Section> Sections { get; set; }
        public List<Employee> Employees { get; set; }
        public void SetDetails(string projectCode, string projectName, bool isStore, bool isHeadOffice)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            IsStore = isStore;
            IsHeadOffice = isHeadOffice;
        }
    }
}

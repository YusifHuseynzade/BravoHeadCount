using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectSections: BaseEntity
    {
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public int SectionId { get; set; }
        public Section? Section { get; set; }
    }
}

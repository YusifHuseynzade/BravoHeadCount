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
        public string PhoneNumber { get; set; }
        public int? ResidentalAreaId { get; set; }
        public ResidentalArea? ResidentalArea { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public int? SubSectionId { get; set; }
        public SubSection? SubSection { get; set; }
        public DateTime StartedDate { get; set; }
        public List<ScheduledData> ScheduledDatas { get; set; }
    }
}

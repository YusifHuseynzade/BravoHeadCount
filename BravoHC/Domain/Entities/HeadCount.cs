using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HeadCount : BaseEntity
    {
        public bool? IsVacant { get; set; } = true;
        public int? ColorId { get; set; }
        public HeadCountBackgroundColor Color { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int? SectionId { get; set; }
        public Section? Section { get; set; }
        public int? SubSectionId { get; set; }
        public SubSection? SubSection { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int HCNumber { get; set; }
        public int? ParentId { get; set; }
        public HeadCount? Parent { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);

        public void SetDetails(bool? isVacant, int projectId, Project project, int sectionId, Section section, int subSectionId, SubSection subSection, int positionId, Position position, int employeeId, Employee employee, int hcNumber, int? parentId)
        {
            IsVacant = isVacant;
            ProjectId = projectId;
            Project = project;
            SectionId = sectionId;
            Section = section;
            SubSectionId = subSectionId;
            SubSection = subSection;
            PositionId = positionId;
            Position = position;
            EmployeeId = employeeId;
            Employee = employee;
            HCNumber = hcNumber;
            ParentId = parentId;
        }
    }
}

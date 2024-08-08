using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HeadCount : BaseEntity
    {
        public bool? IsVacant { get; set; }
        public string? RecruiterComment { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
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

        public void SetDetails(bool? isVacant, string? recruiterComment,int projectId, Project project, int functionalAreaId, FunctionalArea functionalArea, int sectionId, Section section, int subSectionId, SubSection subSection, int positionId, Position position, int employeeId, Employee employee, int hcNumber, int? parentId)
        {
            IsVacant = isVacant;
            RecruiterComment = recruiterComment;
            ProjectId = projectId;
            Project = project;
            FunctionalAreaId = functionalAreaId;
            FunctionalArea = functionalArea;
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

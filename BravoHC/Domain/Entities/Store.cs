using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Store : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        public int FormatId { get; set; }
        public Format Format { get; set; }
        public int HeadCountNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public List<Employee> Employees { get; set; }
        public void SetDetails(int projectId, Project project, int functionalAreaId, FunctionalArea functionalArea, int formatId, Format format, int headCountNumber, List<Employee> employees)
        {
            ProjectId = projectId;
            Project = project;
            FunctionalAreaId = functionalAreaId;
            FunctionalArea = functionalArea;
            FormatId = formatId;
            Format = format;
            HeadCountNumber = headCountNumber;
            Employees = employees;
            ModifiedDate = DateTime.UtcNow.AddHours(4); // Her detay ayarlandığında modified date güncellenir
        }
    }
}

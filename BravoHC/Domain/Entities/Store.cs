using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Store : BaseEntity
    {
        public int? DirectorId { get; set; }
        public Employee Director { get; set; }
        public int? AreaManagerId { get; set; }
        public Employee AreaManager { get; set; }
        public int? StoreManagerId { get; set; }
        public Employee StoreManager { get; set; }
        public int? RecruiterId { get; set; }
        public Employee Recruiter { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        public int FunctionalAreaId { get; set; }
        public FunctionalArea FunctionalArea { get; set; }
        public int? FormatId { get; set; }
        public Format? Format { get; set; }
        public int HeadCountNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public void SetDetails(int projectId, Project project, int functionalAreaId, FunctionalArea functionalArea, int formatId, Format format, int headCountNumber)
        {
            ProjectId = projectId;
            Project = project;
            FunctionalAreaId = functionalAreaId;
            FunctionalArea = functionalArea;
            FormatId = formatId;
            Format = format;
            HeadCountNumber = headCountNumber;
            ModifiedDate = DateTime.UtcNow.AddHours(4); // Her detay ayarlandığında modified date güncellenir
        }
    }
}

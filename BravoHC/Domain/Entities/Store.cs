using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Store : BaseEntity
    {
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        public int HeadCountNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public void SetDetails(int projectId, Project project,  int headCountNumber)
        {
            ProjectId = projectId;
            Project = project;
            HeadCountNumber = headCountNumber;
            ModifiedDate = DateTime.UtcNow.AddHours(4); // Her detay ayarlandığında modified date güncellenir
        }
    }
}

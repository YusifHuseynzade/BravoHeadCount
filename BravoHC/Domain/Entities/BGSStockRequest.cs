using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BGSStockRequest: BaseEntity
    {
        public int UniformId { get; set; }
        public Uniform Uniform { get; set; }
        public int? Count { get; set; }
        public int RequestCount { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public void InitializeBGSStockRequest(int uniformId, Uniform uniform, int projectId, Project project, int requestCount, RequestStatus status, DateTime createdDate, string createdBy)
        {
            UniformId = uniformId;
            Uniform = uniform;
            ProjectId = projectId;
            Project = project;
            RequestCount = requestCount;
            Status = status;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
        }

    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StoreStockRequest : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int UniformId { get; set; }
        public Uniform Uniform { get; set; }
        public int RequestCount { get; set; }
        public int? Count { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public void InitializeStoreStockRequest(int employeeId, Employee employee, int uniformId, Uniform uniform, int requestCount, RequestStatus status, DateTime createdDate, string createdBy)
        {
            EmployeeId = employeeId;
            Employee = employee;
            UniformId = uniformId;
            Uniform = uniform;
            RequestCount = requestCount;
            Status = status;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
        }

    }
}

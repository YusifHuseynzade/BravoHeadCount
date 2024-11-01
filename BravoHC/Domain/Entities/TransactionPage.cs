using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain.Entities
{
    public class TransactionPage: BaseEntity
    {
        public int ProjectId { get; set; } 
        public Project Project { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int UniformId { get; set; }
        public Uniform Uniform { get; set; }
        public int UniCount { get; set; }
        public string Sender { get; set; }
        public DateTime SenderDate { get; set; }
        public string? Receiver { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public List<int> DCStockIds { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsBBgs { get; set; }
        public bool IsFirstDistribution { get; set; }
        public bool? IsEnacted { get; set; }
        public DateTime? EnactedDate { get; set; }
        public  bool? PayrollStatus { get; set; }
        public string? HandoveredBy { get; set; }
        public string? DeductedBy { get; set; }
        public DateTime? DeductedDate { get; set; }
        public TransactionPageStatus TransactionStatus { get; set; }
        public void InitializeTransaction(int projectId, Project project, int employeeId, Employee employee, int uniformId, Uniform uniform, int uniCount, string sender, DateTime senderDate, decimal unitPrice, bool isBBgs, bool isFirstDistribution, TransactionPageStatus transactionStatus)
        {
            ProjectId = projectId;
            Project = project;
            EmployeeId = employeeId;
            Employee = employee;
            UniformId = uniformId;
            Uniform = uniform;
            UniCount = uniCount;
            Sender = sender;
            SenderDate = senderDate;
            UnitPrice = unitPrice;
            IsBBgs = isBBgs;
            IsFirstDistribution = isFirstDistribution;
            TransactionStatus = transactionStatus;
        }
    }
}

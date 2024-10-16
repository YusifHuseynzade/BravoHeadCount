using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Queries.Response
{
    public class GetEmployeeProjectHistoryResponse
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int FromProjectId { get; set; }
        public string FromProjectCode { get; set; }
        public int ToProjectId { get; set; }
        public string ToProjectCode { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}

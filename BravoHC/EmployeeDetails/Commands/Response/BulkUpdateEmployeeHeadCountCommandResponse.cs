using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Commands.Response
{
    public class BulkUpdateEmployeeHeadCountCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int UpdatedCount { get; set; }
        public int CreatedCount { get; set; }
        public List<int> FailedEmployeeIds { get; set; }
    }
}

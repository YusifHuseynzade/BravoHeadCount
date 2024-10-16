using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Queries.Response
{
    public class GetEmployeeHistoryListResponse
    {
        public int TotalEmployeeHistoryCount { get; set; }
        public List<GetEmployeeProjectHistoryResponse> EmployeeHistories { get; set; }
    }
}

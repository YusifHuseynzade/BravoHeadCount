using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Queries.Response
{
    public class GetEmployeeListResponse
    {
        public int TotalEmployeeCount { get; set; }
        public List<GetAllEmployeeQueryResponse> Employees { get; set; }
    }
}

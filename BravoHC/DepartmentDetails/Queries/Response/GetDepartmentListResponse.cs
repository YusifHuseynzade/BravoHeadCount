using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Queries.Response
{
    public class GetDepartmentListResponse
    {
        public int TotalDepartmentCount { get; set; }
        public List<GetAllDepartmentQueryResponse> Departments { get; set; }
    }
}

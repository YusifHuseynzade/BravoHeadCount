using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentalAreaDetails.Queries.Response
{
    public class GetResidentalAreaEmployeeListResponse
    {
        public int TotalResidentalAreaEmployeeCount { get; set; }
        public List<GetResidentalAreaEmployeesQueryResponse> ResidentalAreaEmployees { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuTargetDetails.Queries.Response
{
    public class GetBakuTargetEmployeeListResponse
    {
        public int TotalBakuTargetEmployeeCount { get; set; }
        public List<GetBakuTargetEmployeesQueryResponse> BakuTargetEmployees { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuMetroDetails.Queries.Response
{
    public class GetBakuMetroEmployeeListResponse
    {
        public int TotalBakuMetroEmployeeCount { get; set; }
        public List<GetBakuMetroEmployeesQueryResponse> BakuMetroEmployees { get; set; }
    }
}

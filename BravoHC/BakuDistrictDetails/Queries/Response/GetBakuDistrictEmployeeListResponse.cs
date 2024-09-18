using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuDistrictDetails.Queries.Response
{
    public class GetBakuDistrictEmployeeListResponse
    {
        public int TotalBakuDistrictEmployeeCount { get; set; }
        public List<GetBakuDistrictEmployeesQueryResponse> BakuDistrictEmployees { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuDistrictDetails.Queries.Response
{
    public class GetBakuDistrictListResponse
    {
        public int TotalBakuDistrictCount { get; set; }
        public List<GetAllBakuDistrictQueryResponse> BakuDistricts { get; set; }
    }
}

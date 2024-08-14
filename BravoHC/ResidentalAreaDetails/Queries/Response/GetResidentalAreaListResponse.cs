using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentalAreaDetails.Queries.Response
{
    public class GetResidentalAreaListResponse
    {
        public int TotalResidentalAreaCount { get; set; }
        public List<GetAllResidentalAreaQueryResponse> ResidentalAreas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Queries.Response
{
    public class GetHeadCountListResponse
    {
        public int TotalHeadCount { get; set; }
        public List<GetAllHeadCountQueryResponse> HeadCounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuTargetDetails.Queries.Response
{
    public class GetBakuTargetListResponse
    {
        public int TotalBakuTargetCount { get; set; }
        public List<GetAllBakuTargetQueryResponse> BakuTargets { get; set; }
    }
}

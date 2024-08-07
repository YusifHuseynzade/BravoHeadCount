using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Queries.Response
{
    public class GetAllPositionListQueryResponse
    {
        public int TotalPositionCount { get; set; }
        public List<GetAllPositionQueryResponse> Positions { get; set; }
    }
}

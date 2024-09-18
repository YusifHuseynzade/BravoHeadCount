using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuMetroDetails.Queries.Response
{
    public class GetResidentalAreaListResponse
    {
        public int TotalBakuMetroCount { get; set; }
        public List<GetAllBakuMetroQueryResponse> BakuMetros { get; set; }
    }
}

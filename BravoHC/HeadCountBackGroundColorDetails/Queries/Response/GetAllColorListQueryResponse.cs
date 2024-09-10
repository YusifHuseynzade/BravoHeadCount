using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountBackGroundColorDetails.Queries.Response
{
    public class GetAllColorListQueryResponse
    {
        public int TotalColorCount { get; set; }
        public List<GetAllColorQueryResponse> Colors { get; set; }
    }
}

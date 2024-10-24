using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncashmentDetails.Queries.Response
{
    public class GetAllEncashmentListQueryResponse
    {
        public int TotalEncashmentCount { get; set; }
        public List<GetAllEncashmentQueryResponse> Encashments { get; set; }
    }
}

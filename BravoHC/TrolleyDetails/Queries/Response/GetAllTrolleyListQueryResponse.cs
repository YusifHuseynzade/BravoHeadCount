using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyDetails.Queries.Response
{
    public class GetAllTrolleyListQueryResponse
    {
        public int TotalTrolleyCount { get; set; }
        public List<GetAllTrolleyQueryResponse> Trolleys { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyTypeDetails.Queries.Response
{
    public class GetAllTrolleyTypeListQueryResponse
    {
        public int TotalTrolleyTypeCount { get; set; }
        public List<GetAllTrolleyTypeQueryResponse> TrolleyTypes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatDetails.Queries.Response
{
    public class GetAllFormatListQueryResponse
    {
        public int TotalFormatCount { get; set; }
        public List<GetAllFormatQueryResponse> Formats { get; set; }
    }
}

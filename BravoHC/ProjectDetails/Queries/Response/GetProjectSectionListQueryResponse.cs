using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response
{
    public class GetProjectSectionListQueryResponse
    {
        public int TotalProjectSectionCount { get; set; }
        public List<GetProjectSectionQueryResponse> Sections { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSectionDetails.Queries.Response
{
    public class GetSubSectionListResponse
    {
        public int TotalSubSectionCount { get; set; }
        public List<GetAllSubSectionQueryResponse> SubSections { get; set; }
    }
}

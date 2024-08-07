using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionDetails.Queries.Response
{
    public class GetSectionSubSectionListResponse
    {
        public int TotalSectionSubSectionCount { get; set; }
        public List<GetSectionSubSectionQueryResponse> SectionSubSections { get; set; }
    }
}

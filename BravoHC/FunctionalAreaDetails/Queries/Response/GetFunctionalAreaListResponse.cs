using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Queries.Response
{
    public class GetFunctionalAreaListResponse
    {
        public int TotalFunctionalAreaCount { get; set; }
        public List<GetAllFunctionalAreaQueryResponse> FunctionalAreas { get; set; }
    }
}

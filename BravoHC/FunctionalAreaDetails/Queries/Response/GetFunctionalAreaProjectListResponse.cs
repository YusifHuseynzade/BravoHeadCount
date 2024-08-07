using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Queries.Response
{
    public class GetFunctionalAreaProjectListResponse
    {
        public int TotalFunctionalAreaProjectCount { get; set; }
        public List<GetFunctionalAreaProjectsQueryResponse> FunctionalAreaProjects { get; set; }
    }
}

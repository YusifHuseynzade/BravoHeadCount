using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response
{
    public class GetAllProjectListQueryResponse
    {
        public int TotalProjectCount { get; set; }
        public List<GetAllProjectQueryResponse> Projects { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformDetails.Queries.Response
{
    public class GetAllUniformListQueryResponse
    {
        public int TotalUniformCount { get; set; }
        public List<GetAllUniformQueryResponse> Uniforms { get; set; }
    }
}

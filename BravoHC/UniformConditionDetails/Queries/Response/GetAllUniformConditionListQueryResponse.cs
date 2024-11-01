using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniformConditionDetails.Queries.Response;

namespace UniformConditionDetails.Queries.Response
{
    public class GetAllUniformConditionListQueryResponse
    {
        public int TotalUniformConditionCount { get; set; }
        public List<GetAllUniformConditionQueryResponse> UniformConditions { get; set; }
    }
}

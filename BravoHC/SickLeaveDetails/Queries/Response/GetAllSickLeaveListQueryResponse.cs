using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SickLeaveDetails.Queries.Response
{
    public class GetAllSickLeaveListQueryResponse
    {
        public int TotalSickLeaveCount { get; set; }
        public List<GetAllSickLeaveQueryResponse> SickLeaves { get; set; }
    }
}

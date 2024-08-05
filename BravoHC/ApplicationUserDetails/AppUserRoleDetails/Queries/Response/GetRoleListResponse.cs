using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleDetails.Queries.Response
{
    public class GetRoleListResponse
    {
        public int TotalRoleCount { get; set; }
        public List<GetAllRoleQueryResponse> Roles { get; set; }
    }
}

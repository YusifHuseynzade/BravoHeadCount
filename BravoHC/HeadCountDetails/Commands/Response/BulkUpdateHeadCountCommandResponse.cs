using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Commands.Response
{
    public class BulkUpdateHeadCountCommandResponse
    {
        public bool IsSuccess { get; set; }
        public List<UpdateHeadCountResult> Results { get; set; }
    }
}

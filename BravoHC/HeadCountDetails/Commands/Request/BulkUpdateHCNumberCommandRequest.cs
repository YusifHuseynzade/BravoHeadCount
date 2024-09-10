using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Commands.Request
{
    public class BulkUpdateHCNumberCommandRequest : IRequest<BulkUpdateHCNumberCommandResponse>
    {
        public int ProjectId { get; set; }
        public List<HCNumberUpdateRequest> UpdatedHeadCounts { get; set; }
    }
}

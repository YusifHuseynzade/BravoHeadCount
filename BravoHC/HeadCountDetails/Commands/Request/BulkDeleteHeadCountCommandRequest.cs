using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Commands.Request
{
    public class BulkDeleteHeadCountCommandRequest : IRequest<BulkDeleteHeadCountCommandResponse>
    {
        public List<int> Ids { get; set; }
    }
}

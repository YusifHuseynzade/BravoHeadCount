using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Commands.Request
{
    public class BulkUpdateHeadCountCommandRequest : IRequest<BulkUpdateHeadCountCommandResponse>
    {
        public int ProjectId { get; set; }
        public int FunctionalAreaId { get; set; }
        public int SectionId { get; set; }
        public int? SubSectionId { get; set; }
        public int PositionId { get; set; }
        public int Count { get; set; }
    }
}

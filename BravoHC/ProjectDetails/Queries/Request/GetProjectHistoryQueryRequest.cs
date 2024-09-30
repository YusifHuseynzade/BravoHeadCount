using Common.Constants;
using MediatR;
using ProjectDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Request
{
    public class GetProjectHistoryQueryRequest : IRequest<List<GetListProjectHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int ProjectId { get; set; }
    }
}

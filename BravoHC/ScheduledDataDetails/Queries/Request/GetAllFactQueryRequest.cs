using Common.Constants;
using MediatR;
using ScheduledDataDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Request
{
    public class GetAllFactQueryRequest : IRequest<List<GetAllFactListQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
    }
}

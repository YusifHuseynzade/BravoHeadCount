using Common.Constants;
using MediatR;
using StoreDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDetails.Queries.Request
{
    public class GetStoreHistoryQueryRequest : IRequest<List<GetListStoreHistoryQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int StoreId { get; set; }
    }
}

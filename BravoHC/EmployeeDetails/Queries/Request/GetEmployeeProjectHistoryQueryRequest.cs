using Common.Constants;
using EmployeeDetails.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Queries.Request
{
    public class GetEmployeeProjectHistoryQueryRequest : IRequest<List<GetEmployeeHistoryListResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
        public int EmployeeId { get; set; }
    }
}

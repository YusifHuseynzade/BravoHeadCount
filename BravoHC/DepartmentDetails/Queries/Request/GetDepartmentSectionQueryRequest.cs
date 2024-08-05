using Common.Constants;
using DepartmentDetails.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Queries.Request;

public class GetDepartmentSectionQueryRequest : IRequest<List<GetDepartmentSectionQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int DepartmentId { get; set; }
}

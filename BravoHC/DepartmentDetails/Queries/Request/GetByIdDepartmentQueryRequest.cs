using DepartmentDetails.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Queries.Request;

public class GetByIdDepartmentQueryRequest : IRequest<GetByIdDepartmentQueryResponse>
{
	public int Id { get; set; }
}

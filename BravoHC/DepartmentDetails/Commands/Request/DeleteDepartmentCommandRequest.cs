
using DepartmentDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Commands.Request;

public partial class DeleteDepartmentCommandRequest : IRequest<DeleteDepartmentCommandResponse>
{
	public int Id { get; set; }
}


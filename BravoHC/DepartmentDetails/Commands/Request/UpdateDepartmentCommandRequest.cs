using DepartmentDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Commands.Request;

public partial class UpdateDepartmentCommandRequest : IRequest<UpdateDepartmentCommandResponse>
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int FunctionalAreaId { get; set; }
}
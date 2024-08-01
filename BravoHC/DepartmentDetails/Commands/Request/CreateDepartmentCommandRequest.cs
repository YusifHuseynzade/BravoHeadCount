using DepartmentDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Commands.Request;


public class CreateDepartmentCommandRequest : IRequest<CreateDepartmentCommandResponse>
{
    public string Name { get; set; }
    public int FunctionalAreaId { get; set; }
}

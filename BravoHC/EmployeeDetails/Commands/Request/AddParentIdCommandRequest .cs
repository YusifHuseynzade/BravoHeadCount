using EmployeeDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Commands.Request
{
    public class AddParentIdCommandRequest : IRequest<AddParentIdCommandResponse>
    {
        public List<int> EmployeeIds { get; set; }
    }
}

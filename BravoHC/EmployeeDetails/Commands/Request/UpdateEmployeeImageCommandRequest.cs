using EmployeeDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Commands.Request
{
    public class UpdateEmployeeImageCommandRequest : IRequest<UpdateEmployeeImageCommandResponse>
    {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Commands.Response
{
    public class UpdateEmployeeImageCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Commands.Response
{
	public class CreateEmployeeCommandResponse
	{
		public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformDetails.Commands.Response;

public class UpdateUniformCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

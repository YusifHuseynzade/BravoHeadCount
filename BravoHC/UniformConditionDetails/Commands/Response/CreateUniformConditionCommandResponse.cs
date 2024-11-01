using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformConditionDetails.Commands.Response;

public class CreateUniformConditionCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

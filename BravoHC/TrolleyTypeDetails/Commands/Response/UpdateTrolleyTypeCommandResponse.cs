using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyTypeDetails.Commands.Response;

public class UpdateTrolleyTypeCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

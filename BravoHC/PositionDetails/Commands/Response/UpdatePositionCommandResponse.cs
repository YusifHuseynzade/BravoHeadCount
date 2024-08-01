using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Commands.Response;

public class UpdatePositionCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

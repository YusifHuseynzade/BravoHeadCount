using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SickLeaveDetails.Commands.Response;

public class UpdateSickLeaveCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

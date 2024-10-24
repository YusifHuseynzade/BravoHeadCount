using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncashmentDetails.Commands.Response;

public class CreateEncashmentCommandResponse
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; internal set; }
}

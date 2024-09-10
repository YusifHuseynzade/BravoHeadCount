using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountBackGroundColorDetails.Commands.Response;

public class UpdateColorCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

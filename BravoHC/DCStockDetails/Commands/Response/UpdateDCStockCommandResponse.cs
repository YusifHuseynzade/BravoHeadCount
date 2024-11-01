using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCStockDetails.Commands.Response;

public class UpdateDCStockCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

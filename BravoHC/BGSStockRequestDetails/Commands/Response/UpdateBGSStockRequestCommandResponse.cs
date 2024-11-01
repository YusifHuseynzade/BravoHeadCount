using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGSStockRequestDetails.Commands.Response;

public class UpdateBGSStockRequestCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

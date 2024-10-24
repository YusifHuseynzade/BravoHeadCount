using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndOfMonthReportDetails.Commands.Response;

public class CreateEndOfMonthReportCommandResponse
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; internal set; }
}

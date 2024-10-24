using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Commands.Response;

public class UpdateExpensesReportCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

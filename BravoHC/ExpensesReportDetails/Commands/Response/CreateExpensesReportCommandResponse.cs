using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Commands.Response;

public class CreateExpensesReportCommandResponse
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; internal set; }
}

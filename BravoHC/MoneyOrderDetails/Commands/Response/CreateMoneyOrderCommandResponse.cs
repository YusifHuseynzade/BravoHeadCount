using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyOrderDetails.Commands.Response;

public class CreateMoneyOrderCommandResponse
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; internal set; }
}

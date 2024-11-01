using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionPageDetails.Commands.Response;

public class CreateTransactionPageCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

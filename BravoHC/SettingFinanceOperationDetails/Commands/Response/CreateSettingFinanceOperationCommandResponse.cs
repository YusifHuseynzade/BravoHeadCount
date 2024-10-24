using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingFinanceOperationDetails.Commands.Response;

public class CreateSettingFinanceOperationCommandResponse
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; internal set; }
}

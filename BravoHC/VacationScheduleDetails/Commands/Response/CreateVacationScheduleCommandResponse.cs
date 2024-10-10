using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationScheduleDetails.Commands.Response;

public class CreateVacationScheduleCommandResponse
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; internal set; } 
}

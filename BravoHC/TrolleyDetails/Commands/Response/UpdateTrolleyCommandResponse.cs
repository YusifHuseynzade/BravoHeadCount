﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyDetails.Commands.Response;

public class UpdateTrolleyCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

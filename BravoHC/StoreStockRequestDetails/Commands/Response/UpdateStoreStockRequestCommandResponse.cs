﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreStockRequestDetails.Commands.Response;

public class UpdateStoreStockRequestCommandResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; internal set; }
}

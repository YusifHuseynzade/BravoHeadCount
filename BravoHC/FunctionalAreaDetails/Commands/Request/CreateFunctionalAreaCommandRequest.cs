﻿using FunctionalAreaDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Commands.Request;

public class CreateFunctionalAreaCommandRequest : IRequest<CreateFunctionalAreaCommandResponse>
{
    public string Name { get; set; }

}

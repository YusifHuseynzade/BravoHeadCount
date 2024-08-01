using MediatR;
using PositionDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Commands.Request;

public class CreatePositionCommandRequest : IRequest<CreatePositionCommandResponse>
{
	public string Name { get; set; }
}

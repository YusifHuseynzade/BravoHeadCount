using MediatR;
using PositionDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Queries.Request;

public class GetByIdPositionQueryRequest : IRequest<GetByIdPositionQueryResponse>
{
	public int Id { get; set; }
}

using Common.Constants;
using MediatR;
using PositionDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Queries.Request;

public class GetAllPositionQueryRequest : IRequest<List<GetAllPositionQueryResponse>>
{
	public int Page { get; set; } = 1;
	public ShowMoreDto? ShowMore { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountBackGroundColorDetails.Queries.Response;

public class GetByIdColorQueryResponse
{
	public int Id { get; set; }
	public string ColorHexCode { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuMetroDetails.Queries.Response;

public class GetBakuMetroEmployeesQueryResponse
{
	public int Id { get; set; }
    public string FullName { get; set; }
    public string Badge { get; set; }
    public string PhoneNumber { get; set; }
}

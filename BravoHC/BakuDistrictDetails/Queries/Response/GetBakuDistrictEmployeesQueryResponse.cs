using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuDistrictDetails.Queries.Response;

public class GetBakuDistrictEmployeesQueryResponse
{
	public int Id { get; set; }
    public string FullName { get; set; }
    public string Badge { get; set; }
    public string PhoneNumber { get; set; }
}

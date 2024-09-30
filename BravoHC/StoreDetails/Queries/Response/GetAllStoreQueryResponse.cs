using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDetails.Queries.Response;

public class GetAllStoreQueryResponse
{
	public int Id { get; set; }
    public int ProjectId { get; set; }
    public int HeadCountNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}

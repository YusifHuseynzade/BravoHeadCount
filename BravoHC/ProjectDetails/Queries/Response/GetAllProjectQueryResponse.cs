using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response;

public class GetAllProjectQueryResponse
{
	public int Id { get; set; }
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public bool IsStore { get; set; }
    public bool IsHeadOffice { get; set; }
    public int FunctionalAreaId { get; set; }
}

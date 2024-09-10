using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Queries.Response;

public class GetFunctionalAreaProjectsQueryResponse
{
	public int Id { get; set; }
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public bool IsStore { get; set; }
    public bool IsHeadOffice { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDetails.Queries.Response;

public class GetByIdDepartmentQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int FunctionalAreaId { get; set; }
}

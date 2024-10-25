using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyDetails.Queries.Response;

public class GetAllTrolleyQueryResponse
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int TrolleyTypeId { get; set; }
    public string TrolleyTypeName { get; set; }
    public DateTime CountDate { get; set; }
    public int WorkingTrolleysCount { get; set; }
    public int BrokenTrolleysCount { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
}

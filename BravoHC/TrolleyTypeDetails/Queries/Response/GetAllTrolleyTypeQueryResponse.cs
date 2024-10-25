using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyTypeDetails.Queries.Response;

public class GetAllTrolleyTypeQueryResponse
{
    public int Id { get; set; } // Trolley Type ID
    public string Name { get; set; } // Name of the trolley type
    public string ImageUrl { get; set; } // URL to access the image
    public DateTime CreatedDate { get; set; } // Date when created
    public string CreatedBy { get; set; } // User who created
    public DateTime? ModifiedDate { get; set; } // Date when modified
    public string? ModifiedBy { get; set; } // User who modified
}

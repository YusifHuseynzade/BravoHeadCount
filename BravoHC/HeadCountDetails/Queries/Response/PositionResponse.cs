using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Queries.Response
{
    public class PositionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? JobDescriptionUrl { get; set; }
    }
}

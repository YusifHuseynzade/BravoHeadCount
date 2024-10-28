using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrolleyDetails.Queries.Response
{
    public class GetTrolleyHistoryQueryResponse
    {
        public int Id { get; set; }
        public int TrolleyId { get; set; }
        public int WorkingTrolleysCount { get; set; }
        public int BrokenTrolleysCount { get; set; }
        public DateTime CountDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}

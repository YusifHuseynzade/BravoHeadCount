using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response
{
    public class GetProjectHistoryQueryResponse
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public bool? OldIsActive { get; set; }
        public bool? NewIsActive { get; set; }
        public string OldFormat { get; set; }
        public string NewFormat { get; set; }
        public string OldFunctionalArea { get; set; }
        public string NewFunctionalArea { get; set; }
        public string OldDirector { get; set; }
        public string NewDirector { get; set; }
        public string OldDirectorEmail { get; set; }
        public string NewDirectorEmail { get; set; }
        public string OldAreaManager { get; set; }
        public string NewAreaManager { get; set; }
        public string OldAreaManagerEmail { get; set; }
        public string NewAreaManagerEmail { get; set; }
        public string OldStoreManagerEmail { get; set; }
        public string NewStoreManagerEmail { get; set; }
        public string OldRecruiter { get; set; }
        public string NewRecruiter { get; set; }
        public string OldRecruiterEmail { get; set; }
        public string NewRecruiterEmail { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

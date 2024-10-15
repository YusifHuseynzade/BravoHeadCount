using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadCountDetails.Queries.Response
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Badge { get; set; }
        public string FIN { get; set; }
        public string PhoneNumber { get; set; }
        public string RecruiterComment { get; set; }
        public ResidentalAreaResponse ResidentalArea { get; set; }
        public BakuDistrictResponse BakuDistrict { get; set; }
        public BakuMetroResponse BakuMetro { get; set; }
        public BakuTargetResponse BakuTarget { get; set; }
        public int PositionId { get; set; }
        public int SectionId { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartedDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}

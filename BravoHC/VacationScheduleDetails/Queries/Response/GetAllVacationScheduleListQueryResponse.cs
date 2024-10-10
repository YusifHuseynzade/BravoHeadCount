using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationScheduleDetails.Queries.Response
{
    public class GetAllVacationScheduleListQueryResponse
    {
        public int TotalVacationScheduleCount { get; set; }
        public List<GetAllVacationScheduleQueryResponse> VacationSchedules { get; set; }
    }
}

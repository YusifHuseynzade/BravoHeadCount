using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response
{
    public class GetScheduledDataListResponse
    {
        public int TotalScheduledDataCount { get; set; }
        public string ProjectName { get; set; }
        public List<string> Sections { get; set; }
        public int Week { get; set; }
        public int WeeklyMorningShiftCount { get; set; } = 0;
        public int WeeklyAfterNoonShiftCount { get; set; } = 0;
        public int WeeklyEveningShiftCount { get; set; } = 0;
        public string WeeklyMorningShiftPercentage { get; set; } = "0%";
        public string WeeklyAfterNoonShiftPercentage { get; set; } = "0%";
        public string WeeklyEveningShiftPercentage { get; set; } = "0%";
        public int WeeklyDayOffCount { get; set; } = 0;
        public int WeeklyHolidayCount { get; set; } = 0;
        public int WeeklyVacationCount { get; set; } = 0;
        public List<GetAllScheduledDataQueryResponse> ScheduledDatas { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Queries.Response;

public class GetAllScheduledDataQueryResponse
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeBadge { get; set; }
    public string EmployeeSection { get; set; }
    public string EmployeePosition { get; set; }
    public int MorningShiftCount { get; set; } = 0;
    public int AfterNoonShiftCount { get; set; } = 0;
    public int EveningShiftCount { get; set; } = 0;
    public int DayOffCount { get; set; } = 0;
    public int HolidayBalance { get; set; }
    public int VacationBalance { get; set; }
    public List<ScheduledDataItem> ScheduledData { get; set; }
}

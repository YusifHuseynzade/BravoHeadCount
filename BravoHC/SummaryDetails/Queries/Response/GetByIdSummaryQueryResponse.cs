using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryDetails.Queries.Response;

public class GetByIdSummaryQueryResponse
{
    public int Id { get; set; }
    public EmployeeResponse Employee { get; set; }
    public int MonthId { get; set; }
    public string MonthName { get; set; }
    public int Year { get; set; }
    public int WorkdaysCount { get; set; }
    public int VacationDaysCount { get; set; }
    public int SickDaysCount { get; set; }
    public int DayOffCount { get; set; }
    public int AbsentDaysCount { get; set; }
}
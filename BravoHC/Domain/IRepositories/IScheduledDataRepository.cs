﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IScheduledDataRepository : IRepository<ScheduledData>
    {
        Task<ScheduledData> GetLastScheduledDataAsync();
        Task<IEnumerable<ScheduledData>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ScheduledData> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<List<ScheduledData>> GetByEmployeeAndMonthAsync(int employeeId, int year, int monthId);
    }
}

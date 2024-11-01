﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IHeadCountHistoryRepository : IRepository<HeadCountHistory>
    {
        Task<HeadCountHistory> GetLatestHistoryByEmployeeIdAsync(int employeeId);
    }
}

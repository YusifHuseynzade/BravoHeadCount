﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IEndOfMonthReportRepository : IRepository<EndOfMonthReport>
    {
        Task<List<EndOfMonthReport>> GetAllAsync();
    }
}

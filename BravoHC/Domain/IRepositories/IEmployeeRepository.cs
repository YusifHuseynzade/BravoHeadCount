﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<int?> GetIdByNameAsync(string employeeName);
        Task<List<int>> GetAllEmployeeIdsAsync();
        Task<Employee> GetByBadgeAsync(string badge);
    }
}

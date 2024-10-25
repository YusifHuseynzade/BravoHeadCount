using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByNameAsync(string employeeName)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.FullName == employeeName);

            return employee?.Id; // Eğer proje yoksa null dönecektir
        }

        public async Task<List<int>> GetAllEmployeeIdsAsync()
        {
            return await _context.Employees.Select(e => e.Id).ToListAsync();
        }
        public async Task<Employee> GetByBadgeAsync(string badge)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Badge == badge);
        }
    }
}

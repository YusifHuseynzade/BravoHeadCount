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
    }
}

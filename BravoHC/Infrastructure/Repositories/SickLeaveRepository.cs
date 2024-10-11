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
    public class SickLeaveRepository : Repository<SickLeave>, ISickLeaveRepository
    {
        private readonly AppDbContext _context;

        public SickLeaveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<SickLeave> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Set<SickLeave>()
                .FirstOrDefaultAsync(vs => vs.EmployeeId == employeeId);
        }
    }
}

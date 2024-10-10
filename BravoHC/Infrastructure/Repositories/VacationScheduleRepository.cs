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
    public class VacationScheduleRepository : Repository<VacationSchedule>, IVacationScheduleRepository
    {
        private readonly AppDbContext _context;

        public VacationScheduleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<VacationSchedule> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Set<VacationSchedule>()
                .FirstOrDefaultAsync(vs => vs.EmployeeId == employeeId);
        }
    }
}

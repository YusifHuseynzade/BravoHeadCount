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
    public class SummaryRepository : Repository<Summary>, ISummaryRepository
    {
        private readonly AppDbContext _context;

        public SummaryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Summary> GetByEmployeeAndMonthAsync(int employeeId, int year, int monthId)
        {
            return await _context.Summaries
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId
                                          && s.Year == year
                                          && s.MonthId == monthId);
        }
    }
}

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
    public class ScheduledDataRepository : Repository<ScheduledData>, IScheduledDataRepository
    {
        private readonly AppDbContext _context;

        public ScheduledDataRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ScheduledData> GetLastScheduledDataAsync()
        {
            return await _context.Set<ScheduledData>()
                .OrderByDescending(sd => sd.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ScheduledData>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<ScheduledData>()
                .Where(sd => sd.Date >= startDate && sd.Date <= endDate)
                .ToListAsync();
        }
    }
}

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
        public async Task<ScheduledData> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _context.ScheduledDatas
                .FirstOrDefaultAsync(sd => sd.EmployeeId == employeeId && sd.Date.Date == date.Date);
        }
        public async Task<List<ScheduledData>> GetByEmployeeAndMonthAsync(int employeeId, int year, int monthId)
        {
            return await _context.ScheduledDatas
                .Where(sd => sd.EmployeeId == employeeId
                             && sd.Date.Year == year
                             && sd.Date.Month == monthId)
                .ToListAsync();
        }
        public async Task<List<ScheduledData>> GetByEmployeeIdAsync(int employeeId)
        {
            // Belirli bir çalışana ait tüm ScheduledData kayıtlarını al
            return await _context.ScheduledDatas
                .Where(sd => sd.EmployeeId == employeeId)
                .ToListAsync(); // Asenkron olarak listeyi döndür
        }
    }
}

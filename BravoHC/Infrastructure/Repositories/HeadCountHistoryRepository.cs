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
    public class HeadCountHistoryRepository : Repository<HeadCountHistory>, IHeadCountHistoryRepository
    {
        private readonly AppDbContext _context;

        public HeadCountHistoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HeadCountHistory> GetLatestHistoryByEmployeeIdAsync(int employeeId)
        {
            return await _context.HeadCountHistories
                .Where(h => h.EmployeeId == employeeId)
                .OrderByDescending(h => h.ChangeDate)
                .FirstOrDefaultAsync();
        }
    }
}

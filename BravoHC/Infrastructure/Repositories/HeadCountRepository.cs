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
    public class HeadCountRepository : Repository<HeadCount>, IHeadCountRepository
    {
        private readonly AppDbContext _context;

        public HeadCountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<HeadCount> GetByIdAsync(int id)
        {
            return await _context.HeadCounts.FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<int> GetMaxHCNumberByProjectIdAsync(int projectId)
        {
            var maxHCNumber = await _context.HeadCounts
                .Where(hc => hc.ProjectId == projectId)
                .MaxAsync(hc => hc.HCNumber);

            return maxHCNumber;
        }

    }
}

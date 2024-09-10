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
    public class SubSectionRepository : Repository<SubSection>, ISubSectionRepository
    {
        private readonly AppDbContext _context;

        public SubSectionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByNameAsync(string subSectionName)
        {
            var subSection = await _context.SubSections
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == subSectionName);

            return subSection?.Id;
        }
    }
}

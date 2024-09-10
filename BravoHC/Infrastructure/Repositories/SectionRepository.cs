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
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        private readonly AppDbContext _context;

        public SectionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByNameAsync(string sectionName)
        {
            var section = await _context.Sections
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == sectionName);

            return section?.Id; // Eğer proje yoksa null dönecektir
        }
    }
}

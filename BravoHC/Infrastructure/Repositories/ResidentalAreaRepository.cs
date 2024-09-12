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
    public class ResidentalAreaRepository : Repository<ResidentalArea>, IResidentalAreaRepository
    {
        private readonly AppDbContext _context;

        public ResidentalAreaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ResidentalArea?> GetByNameAsync(string name)
        {
            return await _context.ResidentalAreas
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}

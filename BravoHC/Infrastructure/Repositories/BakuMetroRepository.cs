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
    public class BakuMetroRepository : Repository<BakuMetro>, IBakuMetroRepository
    {
        private readonly AppDbContext _context;

        public BakuMetroRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<BakuMetro?> GetByNameAsync(string name)
        {
            return await _context.BakuMetros
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}

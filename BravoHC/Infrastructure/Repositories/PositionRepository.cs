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
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        private readonly AppDbContext _context;

        public PositionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByNameAsync(string positionName)
        {
            var position = await _context.Positions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == positionName);

            return position?.Id; // Eğer proje yoksa null dönecektir
        }

        public async Task<Position?> GetByNameAsync(string name)
        {
            return await _context.Positions
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}

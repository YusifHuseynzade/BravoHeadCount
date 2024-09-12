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
    public class FunctionalAreaRepository : Repository<FunctionalArea>, IFunctionalAreaRepository
    {
        private readonly AppDbContext _context;

        public FunctionalAreaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByNameAsync(string functionalAreaName)
        {
            var functionalArea = await _context.FunctionalAreas
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == functionalAreaName);

            return functionalArea?.Id; // Eğer proje yoksa null dönecektir
        }

        public async Task<FunctionalArea?> GetByNameAsync(string name)
        {
            return await _context.FunctionalAreas
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}

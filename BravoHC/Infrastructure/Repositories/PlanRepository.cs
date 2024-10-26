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
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly AppDbContext _context;

        public PlanRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Plan> GetByValueAsync(string value)
        {
            return await _context.Set<Plan>()
                .FirstOrDefaultAsync(p => p.Value == value);
        }
        // Id'ye göre Plan getir
        public async Task<Plan> GetByIdAsync(int id)
        {
            return await _context.Set<Plan>().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

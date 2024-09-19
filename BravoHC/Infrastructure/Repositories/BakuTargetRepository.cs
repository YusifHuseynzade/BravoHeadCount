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
    public class BakuTargetRepository:  Repository<BakuTarget>, IBakuTargetRepository
    {
        private readonly AppDbContext _context;

        public BakuTargetRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BakuTarget?> GetByNameAsync(string name)
        {
            return await _context.BakuTargets
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }

    }
} 

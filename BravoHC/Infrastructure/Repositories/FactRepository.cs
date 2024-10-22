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
    public class FactRepository : Repository<Fact>, IFactRepository
    {
        private readonly AppDbContext _context;

        public FactRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DCStockRepository : Repository<DCStock>, IDCStockRepository
    {
        private readonly AppDbContext _context;

        public DCStockRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

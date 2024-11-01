using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StoreStockRequestRepository : Repository<StoreStockRequest>, IStoreStockRequestRepository
    {
        private readonly AppDbContext _context;

        public StoreStockRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

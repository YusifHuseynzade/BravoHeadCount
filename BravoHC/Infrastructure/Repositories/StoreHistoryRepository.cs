using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StoreHistoryRepository : Repository<StoreHistory>, IStoreHistoryRepository
    {
        private readonly AppDbContext _context;

        public StoreHistoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

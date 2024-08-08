using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ScheduledDataRepository : Repository<ScheduledData>, IScheduledDataRepository
    {
        private readonly AppDbContext _context;

        public ScheduledDataRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

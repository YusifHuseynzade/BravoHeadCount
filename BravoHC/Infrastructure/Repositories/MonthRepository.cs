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
    public class MonthRepository : Repository<Month>, IMonthRepository
    {
        private readonly AppDbContext _context;

        public MonthRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Month> GetByNumberAsync(int monthNumber)
        {
            return await _context.Months.FirstOrDefaultAsync(m => m.Number == monthNumber);
        }
    }
}

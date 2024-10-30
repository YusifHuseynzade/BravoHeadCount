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
    public class EndOfMonthReportRepository : Repository<EndOfMonthReport>, IEndOfMonthReportRepository
    {
        private readonly AppDbContext _context;

        public EndOfMonthReportRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<EndOfMonthReport>> GetAllAsync()
        {
            // Veritabanından tüm Expense raporlarını getir
            return await _context.EndOfMonthReports.ToListAsync();
        }
    }
}

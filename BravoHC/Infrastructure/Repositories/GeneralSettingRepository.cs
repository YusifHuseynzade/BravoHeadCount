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
    public class GeneralSettingRepository : Repository<GeneralSetting>, IGeneralSettingRepository
    {
        private readonly AppDbContext _context;

        public GeneralSettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<GeneralSetting?> GetSettingsAsync()
        {
            return await _context.GeneralSettings
                .AsNoTracking()  // Prevents tracking for read-only operations
                .FirstOrDefaultAsync();  // Retrieve the first or default entry
        }
    }
}

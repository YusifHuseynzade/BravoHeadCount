using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<AppUser> GetLoggedInUserAsync(string email)
        {
            return await _context.AppUsers
                .Include(u => u.AppUserRoles)           // AppUserRoles bilgilerini yükle
                .ThenInclude(ur => ur.Role)             // Her bir AppUserRole için Role bilgilerini yükle
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

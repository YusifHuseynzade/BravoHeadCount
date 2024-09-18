using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Store> GetByProjectIdAsync(int projectId)
        {
            // Veritabanından ProjectId'ye göre Store bulmaya çalışıyoruz
            var store = await _context.Stores
                .Include(s => s.Project)                            
                .FirstOrDefaultAsync(s => s.ProjectId == projectId);

            // Eğer proje ID'sine göre mağaza bulunamazsa bir hata fırlatıyoruz
            if (store == null)
            {
                throw new Exception($"No store found with ProjectId {projectId}");
            }

            // Bulunan mağazayı döndürüyoruz
            return store;
        }
    }
}

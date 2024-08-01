using Domain.Entities;
using Domain.IRepositories;
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
    }
}

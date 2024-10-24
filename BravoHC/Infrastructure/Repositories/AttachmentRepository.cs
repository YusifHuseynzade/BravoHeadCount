using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        private readonly AppDbContext _context;

        public AttachmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

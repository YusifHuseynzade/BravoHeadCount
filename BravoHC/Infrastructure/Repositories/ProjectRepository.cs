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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByNameAsync(string projectName)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectName == projectName);

            return project?.Id; // Eğer proje yoksa null dönecektir
        }
        public async Task<Project> GetByNameAsync(string name)
        {
            return await _context.Projects
                .Where(x => x.ProjectCode == name)
                .FirstOrDefaultAsync();
        }
        public async Task<Project> GetByProjectCodeAsync(string projectCode)
        {
            return await _context.Projects
                                 .FirstOrDefaultAsync(p => p.ProjectCode == projectCode);
        }
    }
}

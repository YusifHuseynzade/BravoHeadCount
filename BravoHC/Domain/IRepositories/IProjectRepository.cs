using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<int?> GetIdByNameAsync(string projectName);
        Task<Project> GetByNameAsync(string name);
        Task<Project> GetByProjectCodeAsync(string projectCode);
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ISubSectionRepository : IRepository<SubSection>
    {
        Task<int?> GetIdByNameAsync(string subSectionName);
        Task<SubSection?> GetByNameAsync(string name);
    }
}

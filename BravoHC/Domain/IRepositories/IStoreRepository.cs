using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetByProjectIdAsync(int projectId);
    }
}

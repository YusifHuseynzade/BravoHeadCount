using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IFunctionalAreaRepository : IRepository<FunctionalArea>
    {
        Task<int?> GetIdByNameAsync(string functionalAreaName);
        Task<FunctionalArea> GetByNameAsync(string name);
    }
}

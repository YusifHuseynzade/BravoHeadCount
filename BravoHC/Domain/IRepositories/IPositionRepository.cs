using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<int?> GetIdByNameAsync(string positionName);
        Task<Position> GetByNameAsync(string name);

    }
}

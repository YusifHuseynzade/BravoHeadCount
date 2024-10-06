using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IHeadCountBackgroundColorRepository : IRepository<HeadCountBackgroundColor>
    {
        Task<int> GetYellowColorIdAsync();
        Task<int> GetWhiteColorIdAsync();
        Task<int> GetBlueColorIdAsync();
    }
}

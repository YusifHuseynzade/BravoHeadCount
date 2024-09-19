using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBakuDistrictRepository : IRepository<BakuDistrict>
    {
        Task<BakuDistrict> GetByNameAsync(string name);
    }
}

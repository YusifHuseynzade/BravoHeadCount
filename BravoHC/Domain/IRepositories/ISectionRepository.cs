﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<int?> GetIdByNameAsync(string sectionName);
        Task<Section> GetByNameAsync(string name);
    }
}

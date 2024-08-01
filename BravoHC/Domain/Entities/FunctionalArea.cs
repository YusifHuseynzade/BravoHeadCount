﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FunctionalArea : BaseEntity
    {
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}
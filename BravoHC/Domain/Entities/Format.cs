﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Format: BaseEntity
    {
        public string Name { get; set; }
        public List<Store> Stores { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrolleyHistory: BaseEntity
    {
        public int TrolleyId { get; set; }
        public int WorkingTrolleysCount { get; set; }
        public int BrokenTrolleysCount { get; set; }
        public DateTime CountDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string ModifiedBy { get; set; } 
    }
}

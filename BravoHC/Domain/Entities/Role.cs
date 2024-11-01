﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string RoleLevel { get; set; }
        public List<AppUserRole>? AppUserRoles { get; set; }
        public void SetDetail(string name)
        {
            this.RoleName = name;
        }
    }
}

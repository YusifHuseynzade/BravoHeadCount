﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
        public DbSet<HeadCount> HeadCounts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ProjectSections> ProjectSections { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ScheduledData> ScheduledDatas { get; set; }
        public DbSet<ResidentalArea> ResidentalAreas { get; set; }
        public DbSet<HeadCountBackgroundColor> HeadCountBackgroundColors { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

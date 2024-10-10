using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HeadCount> HeadCounts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ProjectSections> ProjectSections { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ResidentalArea> ResidentalAreas { get; set; }
        public DbSet<BakuDistrict> BakuDistricts { get; set; }
        public DbSet<BakuTarget> BakuTargets { get; set; }
        public DbSet<BakuMetro> BakuMetros { get; set; }
        public DbSet<HeadCountBackgroundColor> HeadCountBackgroundColors { get; set; }
        public DbSet<StoreHistory> StoreHistories { get; set; }
        public DbSet<ProjectHistory> ProjectHistories { get; set; }
        public DbSet<HeadCountHistory> HeadCountHistories { get; set; }
        public DbSet<ScheduledData> ScheduledDatas { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<SickLeave> SickLeaves { get; set; }
        public DbSet<VacationSchedule> VacationSchedules { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Month> Months { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

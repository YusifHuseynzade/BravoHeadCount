using Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        // Seed method to add initial data
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Months.Any())
            {
                var months = new List<Month>
                {
                    new Month { Number = 1, Name = "Ocak" },
                    new Month { Number = 2, Name = "Şubat" },
                    new Month { Number = 3, Name = "Mart" },
                    new Month { Number = 4, Name = "Nisan" },
                    new Month { Number = 5, Name = "Mayıs" },
                    new Month { Number = 6, Name = "Haziran" },
                    new Month { Number = 7, Name = "Temmuz" },
                    new Month { Number = 8, Name = "Ağustos" },
                    new Month { Number = 9, Name = "Eylül" },
                    new Month { Number = 10, Name = "Ekim" },
                    new Month { Number = 11, Name = "Kasım" },
                    new Month { Number = 12, Name = "Aralık" }
                };

                await context.Months.AddRangeAsync(months);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedPlansAsync(AppDbContext context)
        {
            if (!context.Plans.Any())
            {
                var plans = new List<Plan>
            {
            new Plan { Value = "06:30-15:30", Label = "06:30-15:30", Color = "#EAE3C8", Shift = "Səhər" },
            new Plan { Value = "07:00-16:00", Label = "07:00-16:00", Color = "#CFC5A5", Shift = "Səhər" },
            new Plan { Value = "07:30-16:30", Label = "07:30-16:30", Color = "#E3D18A", Shift = "Səhər" },
            new Plan { Value = "08:00-17:00", Label = "08:00-17:00", Color = "#BD9354", Shift = "Səhər" },
            new Plan { Value = "08:00-13:00", Label = "08:00-13:00", Color = "#9E7540", Shift = "Səhər" },
            new Plan { Value = "09:00-18:00", Label = "09:00-18:00", Color = "#D8EFD3", Shift = "Səhər" },
            new Plan { Value = "10:00-19:00", Label = "10:00-19:00", Color = "#95D2B3", Shift = "Səhər" },
            new Plan { Value = "11:00-20:00", Label = "11:00-20:00", Color = "#55AD9B", Shift = "Günorta" },
            new Plan { Value = "12:00-21:00", Label = "12:00-21:00", Color = "#F1F8E8", Shift = "Günorta" },
            new Plan { Value = "12:30-21:30", Label = "12:30-21:30", Color = "#116A7B", Shift = "Günorta" },
            new Plan { Value = "13:00-22:00", Label = "13:00-22:00", Color = "#CDC2AE", Shift = "Gecə" },
            new Plan { Value = "13:30-22:30", Label = "13:30-22:30", Color = "#6096B4", Shift = "Gecə" },
            new Plan { Value = "14:00-23:00", Label = "14:00-23:00", Color = "#DBA39A", Shift = "Gecə" },
            new Plan { Value = "14:30-23:30", Label = "14:30-23:30", Color = "#F0DBDB", Shift = "Gecə" },
            new Plan { Value = "15:00-23:59", Label = "15:00-23:59", Color = "#F5EBE0", Shift = "Gecə" },
            new Plan { Value = "16:00-21:00", Label = "16:00-21:00", Color = "#BA94D1", Shift = "Günorta" },
            new Plan { Value = "17:00-01:00", Label = "17:00-01:00", Color = "#BCCEF8", Shift = "Gecə" },
            new Plan { Value = "22:00-07:00", Label = "22:00-07:00", Color = "#92A9BD", Shift = "Gecə" },
            new Plan { Value = "22:00-08:00", Label = "22:00-08:00", Color = "#E4CDA7", Shift = "Gecə" },
            new Plan { Value = "22:30-07:30", Label = "22:30-07:30", Color = "#D3E4CD", Shift = "Gecə" },
            new Plan { Value = "23:59-09:00", Label = "23:59-09:00", Color = "#C37B89", Shift = "Gecə" },
            new Plan { Value = "Bayram", Label = "Bayram", Color = "#000000", Shift = "Bayram" },
            new Plan { Value = "Məzuniyyət", Label = "Məzuniyyət", Color = "#000000", Shift = "Bayram" }
            };

                await context.Plans.AddRangeAsync(plans);
                await context.SaveChangesAsync();
            }
        }
    }
}

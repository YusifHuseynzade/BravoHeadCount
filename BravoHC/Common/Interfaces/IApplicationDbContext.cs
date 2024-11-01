﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
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
        public DbSet<Fact> Facts { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<EmployeeBalance> EmployeeBalances { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Encashment> Encashments { get; set; }
        public DbSet<EndOfMonthReport> EndOfMonthReports { get; set; }
        public DbSet<ExpensesReport> ExpensesReports { get; set; }
        public DbSet<MoneyOrder> MoneyOrders { get; set; }
        public DbSet<SettingFinanceOperation> SettingFinanceOperations { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Trolley> Trolleys { get; set; }
        public DbSet<TrolleyType> TrolleyTypes { get; set; }
        public DbSet<EncashmentHistory> EncashmentHistories { get; set; }
        public DbSet<EndOfMonthReportHistory> EndOfMonthReportHistories { get; set; }
        public DbSet<ExpensesReportHistory> ExpensesReportHistories { get; set; }
        public DbSet<MoneyOrderHistory> MoneyOrderHistories { get; set; }
        public DbSet<TrolleyHistory> TrolleyHistories { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<DCStock> DCStocks { get; set; }
        public DbSet<BGSStockRequest> BGSStockRequests { get; set; }
        public DbSet<StoreStockRequest> StoreStockRequests { get; set; }
        public DbSet<TransactionPage> Transactions { get; set; }
        public DbSet<Uniform> Uniforms { get; set; }
        public DbSet<UniformCondition> UniformConditions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

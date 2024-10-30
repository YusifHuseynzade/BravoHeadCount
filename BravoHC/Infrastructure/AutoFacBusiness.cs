using ApplicationUserDetails.Profiles;
using Autofac;
using AutoMapper;
using BakuDistrictDetails.Profiles;
using BakuMetroDetails.Profiles;
using BakuTargetDetails.Profiles;
using Domain.IRepositories;
using Domain.IServices;
using EmployeeDetails.Handlers.CommandHandlers;
using EmployeeDetails.Profiles;
using EncashmentDetails.Profiles;
using EndOfMonthReportDetails.Profiles;
using ExpensesReportDetails.Profiles;
using GeneralSettingDetails.Profiles;
using HeadCountBackGroundColorDetails.Profiles;
using HeadCountDetails.HeadCountExportedService;
using HeadCountDetails.Profiles;
using Infrastructure.BackgroundServices;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MoneyOrderDetails.Profiles;
using PositionDetails.Profiles;
using ProjectDetails.Profiles;
using ResidentalAreaDetails.Profiles;
using ScheduledDataBackgroundService;
using ScheduledDataDetails.Handlers.CommandHandlers;
using ScheduledDataDetails.Profiles;
using ScheduledDataDetails.ScheduledDataExportService;
using SectionDetails.Profiles;
using SickLeaveDetails.Profiles;
using StoreDetails.Profiles;
using SubSectionDetails.Profiles;
using SummaryDetails.Profiles;
using TrolleyDetails.Profiles;
using TrolleyTypeDetails.Profiles;
using VacationScheduleDetails.Profiles;

namespace Infrastructure
{
    public class AutoFacBusiness : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<AppUserRoleRepository>().As<IAppUserRoleRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<EmployeeBalanceRepository>().As<IEmployeeBalanceRepository>();
            builder.RegisterType<HeadCountRepository>().As<IHeadCountRepository>();
            builder.RegisterType<HeadCountHistoryRepository>().As<IHeadCountHistoryRepository>();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<ProjectHistoryRepository>().As<IProjectHistoryRepository>();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>();
            builder.RegisterType<ProjectSectionsRepository>().As<IProjectSectionsRepository>();
            builder.RegisterType<StoreRepository>().As<IStoreRepository>();
            builder.RegisterType<StoreHistoryRepository>().As<IStoreHistoryRepository>();
            builder.RegisterType<SubSectionRepository>().As<ISubSectionRepository>();
            builder.RegisterType<ResidentalAreaRepository>().As<IResidentalAreaRepository>();
            builder.RegisterType<BakuDistrictRepository>().As<IBakuDistrictRepository>();
            builder.RegisterType<BakuMetroRepository>().As<IBakuMetroRepository>();
            builder.RegisterType<BakuTargetRepository>().As<IBakuTargetRepository>();
            builder.RegisterType<HeadCountBackgroundColorRepository>().As<IHeadCountBackgroundColorRepository>();
            builder.RegisterType<ScheduledDataRepository>().As<IScheduledDataRepository>();
            builder.RegisterType<PlanRepository>().As<IPlanRepository>();
            builder.RegisterType<FactRepository>().As<IFactRepository>();
            builder.RegisterType<SummaryRepository>().As<ISummaryRepository>();
            builder.RegisterType<SickLeaveRepository>().As<ISickLeaveRepository>();
            builder.RegisterType<MonthRepository>().As<IMonthRepository>();
            builder.RegisterType<VacationScheduleRepository>().As<IVacationScheduleRepository>();
            builder.RegisterType<AttachmentRepository>().As<IAttachmentRepository>();
            builder.RegisterType<EncashmentRepository>().As<IEncashmentRepository>();
            builder.RegisterType<EndOfMonthReportRepository>().As<IEndOfMonthReportRepository>();
            builder.RegisterType<ExpensesReportRepository>().As<IExpensesReportRepository>();
            builder.RegisterType<MoneyOrderRepository>().As<IMoneyOrderRepository>();
            builder.RegisterType<SettingFinanceOperationRepository>().As<ISettingFinanceOperationRepository>();
            builder.RegisterType<TrolleyRepository>().As<ITrolleyRepository>();
            builder.RegisterType<TrolleyTypeRepository>().As<ITrolleyTypeRepository>();
            builder.RegisterType<EncashmentHistoryRepository>().As<IEncashmentHistoryRepository>();
            builder.RegisterType<EndOfMonthReportHistoryRepository>().As<IEndOfMonthReportHistoryRepository>();
            builder.RegisterType<ExpensesReportHistoryRepository>().As<IExpensesReportHistoryRepository>();
            builder.RegisterType<MoneyOrderHistoryRepository>().As<IMoneyOrderHistoryRepository>();
            builder.RegisterType<TrolleyHistoryRepository>().As<ITrolleyHistoryRepository>();
            builder.RegisterType<GeneralSettingRepository>().As<IGeneralSettingRepository>();
            builder.RegisterType<SmsService>().As<ISmsService>();
            builder.RegisterType<HeadCountExportService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ScheduledDataExportService>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<EmployeeHeadCountService>()
            //   .As<IHostedService>()  // IHostedService olarak kaydet
            //   .SingleInstance();
            //builder.RegisterType<ScheduledDataCronJobService>()
            // .As<IHostedService>()  // IHostedService olarak kaydet
            // .SingleInstance();
            //builder.RegisterType<SummaryCronJobService>()
            //.As<IHostedService>()  // IHostedService olarak kaydet
            //.SingleInstance();
            //builder.RegisterType<AttendanceBackgroundService>()
            //.As<IHostedService>()  // IHostedService olarak kaydet
            //.SingleInstance();
            builder.RegisterType<EmployeeProjectChangeChecker>()
            .As<IHostedService>()  // IHostedService olarak kaydet
            .SingleInstance();
            builder.RegisterType<GeneralSettingsCronJobService>()
           .As<IHostedService>()  // IHostedService olarak kaydet
           .SingleInstance();


            builder.Register(ctx =>
            {
                var httpContextAccessor = ctx.Resolve<IHttpContextAccessor>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AppUserMapper(httpContextAccessor));
                    cfg.AddProfile(new ProjectMapper(httpContextAccessor));
                    cfg.AddProfile(new SectionMapper(httpContextAccessor));
                    cfg.AddProfile(new SubSectionMapper(httpContextAccessor));
                    cfg.AddProfile(new PositionMapper(httpContextAccessor));
                    cfg.AddProfile(new StoreMapper(httpContextAccessor));
                    cfg.AddProfile(new EmployeeMapper(httpContextAccessor));
                    cfg.AddProfile(new HeadCountMapper(httpContextAccessor));
                    cfg.AddProfile(new ResidentalAreaMapper(httpContextAccessor));
                    cfg.AddProfile(new BakuDistrictMapper(httpContextAccessor));
                    cfg.AddProfile(new BakuMetroMapper(httpContextAccessor));
                    cfg.AddProfile(new BakuTargetMapper(httpContextAccessor));
                    cfg.AddProfile(new ColorMapper(httpContextAccessor));
                    cfg.AddProfile(new SickLeaveMapper(httpContextAccessor));
                    cfg.AddProfile(new ScheduledDataMapper(httpContextAccessor));
                    cfg.AddProfile(new SummaryMapper(httpContextAccessor));
                    cfg.AddProfile(new VacationScheduleMapper(httpContextAccessor));
                    cfg.AddProfile(new EncashmentMapper(httpContextAccessor));
                    cfg.AddProfile(new ExpensesReportMapper(httpContextAccessor));
                    cfg.AddProfile(new EndOfMonthReportMapper(httpContextAccessor));
                    cfg.AddProfile(new MoneyOrderMapper(httpContextAccessor));
                    cfg.AddProfile(new TrolleyMapper(httpContextAccessor));
                    cfg.AddProfile(new TrolleyTypeMapper(httpContextAccessor));
                    cfg.AddProfile(new GeneralSettingMapper(httpContextAccessor));
                });

                return config;
            }).SingleInstance() // MapperConfiguration nesnesini tekil olarak kaydet
          .AsSelf();

            builder.Register(ctx =>
            {
                // MapperConfiguration nesnesi otomatik olarak çözümlenecek
                var configuration = ctx.Resolve<MapperConfiguration>();
                return configuration.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();

        }
    }
}

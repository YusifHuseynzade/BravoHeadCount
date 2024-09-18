using ApplicationUserDetails.Profiles;
using Autofac;
using AutoMapper;
using BakuDistrictDetails.Profiles;
using BakuMetroDetails.Profiles;
using BakuTargetDetails.Profiles;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using EmployeeDetails.Profiles;
using HeadCountBackGroundColorDetails.Profiles;
using HeadCountDetails.HeadCountExportedService;
using HeadCountDetails.Profiles;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using PositionDetails.Profiles;
using ProjectDetails.Profiles;
using ResidentalAreaDetails.Profiles;
using SectionDetails.Profiles;
using StoreDetails.Profiles;
using SubSectionDetails.Profiles;

namespace Infrastructure
{
    public class AutoFacBusiness : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<HeadCountRepository>().As<IHeadCountRepository>();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
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
            builder.RegisterType<SmsService>().As<ISmsService>();
            builder.RegisterType<HeadCountExportService>().AsSelf().InstancePerLifetimeScope();

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

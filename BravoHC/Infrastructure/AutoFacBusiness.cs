using ApplicationUserDetails.Profiles;
using Autofac;
using AutoMapper;
using Domain.IRepositories;
using Domain.IServices;
using EmployeeDetails.Profiles;
using FormatDetails.Profiles;
using FunctionalAreaDetails.Profiles;
using HeadCountDetails.Profiles;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using PositionDetails.Profiles;
using ProjectDetails.Profiles;
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
            builder.RegisterType<FormatRepository>().As<IFormatRepository>();
            builder.RegisterType<FunctionalAreaRepository>().As<IFunctionalAreaRepository>();
            builder.RegisterType<HeadCountRepository>().As<IHeadCountRepository>();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>();
            builder.RegisterType<StoreRepository>().As<IStoreRepository>();
            builder.RegisterType<SubSectionRepository>().As<ISubSectionRepository>();
            builder.RegisterType<ScheduledDataRepository>().As<IScheduledDataRepository>();
            builder.RegisterType<SmsService>().As<ISmsService>();

            builder.Register(ctx =>
            {
                var httpContextAccessor = ctx.Resolve<IHttpContextAccessor>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AppUserMapper(httpContextAccessor));
                    cfg.AddProfile(new FormatMapper(httpContextAccessor));
                    cfg.AddProfile(new FunctionalAreaMapper(httpContextAccessor));
                    cfg.AddProfile(new ProjectMapper(httpContextAccessor));
                    cfg.AddProfile(new SectionMapper(httpContextAccessor));
                    cfg.AddProfile(new SubSectionMapper(httpContextAccessor));
                    cfg.AddProfile(new PositionMapper(httpContextAccessor));
                    cfg.AddProfile(new StoreMapper(httpContextAccessor));
                    cfg.AddProfile(new EmployeeMapper(httpContextAccessor));
                    cfg.AddProfile(new HeadCountMapper(httpContextAccessor));

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

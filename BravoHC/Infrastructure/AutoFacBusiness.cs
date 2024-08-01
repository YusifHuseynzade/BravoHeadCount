﻿using ApplicationUserDetails.Profiles;
using Autofac;
using AutoMapper;
using Domain.IRepositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Infrastructure
{
    public class AutoFacBusiness : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<FormatRepository>().As<IFormatRepository>();
            builder.RegisterType<FunctionalAreaRepository>().As<IFunctionalAreaRepository>();
            builder.RegisterType<HeadCountRepository>().As<IHeadCountRepository>();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>();
            builder.RegisterType<StoreRepository>().As<IStoreRepository>();
            builder.RegisterType<SubSectionRepository>().As<ISubSectionRepository>();

            builder.Register(ctx =>
            {
                var httpContextAccessor = ctx.Resolve<IHttpContextAccessor>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AppUserMapper(httpContextAccessor));

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

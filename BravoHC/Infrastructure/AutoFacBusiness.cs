using ApplicationUserDetails.Profiles;
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

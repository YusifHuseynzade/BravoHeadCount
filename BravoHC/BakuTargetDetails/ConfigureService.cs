using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BakuTargetDetails
{
    public static class ConfigureService
    {
        public static IServiceCollection AddBakuTargetServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}

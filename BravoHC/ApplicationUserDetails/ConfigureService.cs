using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppUserDetails
{
    public static class ConfigureService
    {
        public static IServiceCollection AddAppUserServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}

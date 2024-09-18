using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ResidentalAreaDetails
{
    public static class ConfigureService
    {
        public static IServiceCollection AddBakuDistrictServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}

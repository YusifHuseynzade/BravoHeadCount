using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FunctionalAreaDetails
{
    public static class ConfigureService
    {
        public static IServiceCollection AddFunctionalAreaServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}

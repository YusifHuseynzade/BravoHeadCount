using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DepartmentDetails
{
    public static class ConfigureService
    {
        public static IServiceCollection AddDepartmentServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}

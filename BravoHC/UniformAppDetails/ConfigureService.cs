using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UniformDetails;

public static class ConfigureService
{
    public static IServiceCollection AddUniformServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
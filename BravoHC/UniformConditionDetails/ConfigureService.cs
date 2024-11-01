using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UniformConditionDetails;

public static class ConfigureService
{
    public static IServiceCollection AddUniformConditionServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
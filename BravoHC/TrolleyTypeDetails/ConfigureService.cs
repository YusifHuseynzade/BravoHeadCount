using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TrolleyTypeDetails;

public static class ConfigureService
{
    public static IServiceCollection AddTrolleyTypeServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
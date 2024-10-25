using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TrolleyDetails;

public static class ConfigureService
{
    public static IServiceCollection AddTrolleyServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DCStockDetails;

public static class ConfigureService
{
    public static IServiceCollection AddDCStockServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
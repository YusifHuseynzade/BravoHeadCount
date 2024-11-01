using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StoreStockRequestDetails;

public static class ConfigureService
{
    public static IServiceCollection AddStoreStockRequestServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
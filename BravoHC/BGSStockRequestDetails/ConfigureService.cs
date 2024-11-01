using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BGSStockRequestDetails;

public static class ConfigureService
{
    public static IServiceCollection AddBGSStockRequestServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
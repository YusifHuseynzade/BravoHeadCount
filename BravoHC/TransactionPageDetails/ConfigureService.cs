using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TransactionPageDetails;

public static class ConfigureService
{
    public static IServiceCollection AddTransactionPageServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
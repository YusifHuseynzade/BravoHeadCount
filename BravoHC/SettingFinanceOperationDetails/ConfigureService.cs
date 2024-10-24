using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SettingFinanceOperationDetails;

public static class ConfigureService
{
    public static IServiceCollection AddSettingFinanceOperationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails;

public static class ConfigureService
{
	public static IServiceCollection AddEmployeeServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
		});
		return services;
	}
}
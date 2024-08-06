using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;
using Work360.Services.Employee.Infrastructure.Postgres;
using Work360.Services.Employee.Infrastructure.Services;

namespace Work360.Services.Employee.Infrastructure;

public static class Extensions
{
   public static IServiceCollection AddInfrastructure(this IServiceCollection services)
   {
      services.AddSingleton<IEventMapper, EventMapper>();
      services.AddTransient<IEmployeeRepository, EmployeeRepository>();
      services.AddTransient<IMessageBroker, MessageBroker>();

      services.AddNpgsql<AppDbContext>("");
      return services;
   }
}
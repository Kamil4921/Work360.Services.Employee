using Microsoft.EntityFrameworkCore;
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
      services.AddSingleton<IMessageBroker, MessageBroker>();

      services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
         opt.UseNpgsql("Server=172.18.0.3;Port=5432;Database=postgres;User ID=postgres;Password=password;"));
      
      return services;
   }
}
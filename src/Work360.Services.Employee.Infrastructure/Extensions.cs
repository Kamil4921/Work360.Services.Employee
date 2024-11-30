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
      //services.AddTransient<IMessageBroker, MessageBroker>();
      services.AddSingleton<IMessageBroker, MessageBrokerASB>();

      services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
         opt.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=password;"));
      
      return services;
   }
}
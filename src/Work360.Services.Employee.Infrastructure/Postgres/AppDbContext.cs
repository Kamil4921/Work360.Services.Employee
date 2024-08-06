using Microsoft.EntityFrameworkCore;

namespace Work360.Services.Employee.Infrastructure.Postgres;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Core.Entities.Employee> Employees { get; set; }
}
using Microsoft.EntityFrameworkCore;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Infrastructure.Postgres;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<Core.Entities.Employee?> GetEmployee(Guid id)
    {
        return await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Core.Entities.Employee>> GetEmployees()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task AddEmployee(Core.Entities.Employee employee)
    {
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync(); 
    }

    public async Task UpdateEmployee(Core.Entities.Employee employee)
    {
        employee.Version++;
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
    }

    public async Task DeleteEmployee(Guid id)
    {
        var employeeToDelete = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employeeToDelete is not null)
        {
            context.Employees.Remove(employeeToDelete);
            await context.SaveChangesAsync();
        }
    }
}
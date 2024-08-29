using Microsoft.EntityFrameworkCore;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Infrastructure.Postgres;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Core.Entities.Employee?> GetEmployee(Guid id)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Core.Entities.Employee>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task AddEmployee(Core.Entities.Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateEmployee(Core.Entities.Employee employee)
    {
        employee.Version++;
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployee(Guid id)
    {
        var employeeToDelete = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employeeToDelete is not null)
        {
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
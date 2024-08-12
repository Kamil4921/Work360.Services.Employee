using Microsoft.EntityFrameworkCore;
using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Infrastructure.Postgres;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Core.Entities.Employee?> GetEmployee(Guid id)
    {
        try
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Core.Entities.Employee>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task AddEmployee(Core.Entities.Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task UpdateEmployee(Core.Entities.Employee employee)
    {
        var employeeToUpdate = await _context.Employees.FindAsync(employee.Id);
        
        employeeToUpdate.Address = string.IsNullOrEmpty(employee.Address) ? employeeToUpdate.Address : employee.Address;
        
        /*var employeeToUpdate = new Core.Entities.Employee(employee.PESEL, employee.Email, employee.Position,
            employee.Salary, employee.HiredAt, employee.FullName, employee.Address, employee.TypeOfContract,
            employee.State, employee.CreatedAt);
        _context.Employees.Remove(currentEmployee);
*/
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployee(Guid id)
    {
        var employeeToDelete = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employeeToDelete is not null)
        {
            _context.Employees.Remove(employeeToDelete);            
        }
    }
}
namespace Work360.Services.Employee.Core.Repositories;

public interface IEmployeeRepository{
    Task<Entities.Employee> GetEmployee(long id);
    Task<IEnumerable<Entities.Employee>> GetEmployees();
    Task AddEmployee(Entities.Employee employee);
    Task UpdateEmployee(Entities.Employee employee);
    Task DeleteEmployee(long id);
}
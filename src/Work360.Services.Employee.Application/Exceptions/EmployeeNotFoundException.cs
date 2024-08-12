namespace Work360.Services.Employee.Application.Exceptions;

public class EmployeeNotFoundException(Guid employeeId) : ApplicationException($"Can't find an employee with id: {employeeId}.")
{
    public override string Code => "employee_not_found";
}
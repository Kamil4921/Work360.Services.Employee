namespace Work360.Services.Employee.Application.Exceptions;

public class CannotChangeEmployeeStateException(long employeeId, string state) : ApplicationException($"Status of employee with id: {employeeId} can't be changed to state: {state}.")
{
    public override string Code => "cannot_change_employee_state";
}
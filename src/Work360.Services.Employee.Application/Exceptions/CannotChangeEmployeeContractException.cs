namespace Work360.Services.Employee.Application.Exceptions;

public class CannotChangeEmployeeContractException(long employeeId, string contract) : ApplicationException($"Type of contract for employee with id:{employeeId}, can't be changed to: {contract}")
{
    public override string Code => "cannot_change_employee_type_of_contract";
}
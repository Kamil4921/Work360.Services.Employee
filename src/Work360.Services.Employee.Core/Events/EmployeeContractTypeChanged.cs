using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Core.Services;

public class EmployeeContractTypeChanged(Employee employee, Contract previousContract) : IDomainEvent
{
    public Employee Employee { get; } = employee;
    public Contract PreviousContract { get; } = previousContract;
}
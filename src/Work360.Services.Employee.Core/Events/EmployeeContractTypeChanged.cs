using Work360.Services.Employee.Core.Entities;

namespace Work360.Services.Employee.Core.Events;

public class EmployeeContractTypeChanged(Entities.Employee employee, Contract previousContract) : IDomainEvent
{
    public Entities.Employee Employee { get; } = employee;
    public Contract PreviousContract { get; } = previousContract;
}
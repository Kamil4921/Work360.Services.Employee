namespace Work360.Services.Employee.Core.Events;

public class EmployeeUpdated(Entities.Employee employee, Entities.Employee previousEmployee) : IDomainEvent
{
    public Entities.Employee Employee { get; } = employee;
    public Entities.Employee PreviousEmployee { get; } = previousEmployee;
}
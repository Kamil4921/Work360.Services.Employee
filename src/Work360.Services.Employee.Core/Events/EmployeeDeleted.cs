namespace Work360.Services.Employee.Core.Events;

public class EmployeeDeleted(Entities.Employee employee) : IDomainEvent
{
    public Entities.Employee Employee { get; } = employee;
}
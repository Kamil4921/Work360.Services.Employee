using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Core.Services;

namespace Work360.Services.Employee.Core.Events;

public class EmployeeStateChanged(Entities.Employee employee, State previousState) : IDomainEvent
{
    public Entities.Employee Employee { get; } = employee;
    public State PreviousState { get; } = previousState;
}
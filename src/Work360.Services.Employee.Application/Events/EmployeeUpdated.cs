using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeUpdated(Core.Entities.Employee employee, Core.Entities.Employee previousEmployee) : INotification
{
    public Core.Entities.Employee Employee { get; } = employee;
    public Core.Entities.Employee PreviousEmployee { get; } = previousEmployee;
}
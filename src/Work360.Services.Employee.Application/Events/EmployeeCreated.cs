using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeCreated(Guid employeeId, string employeeFullName) : INotification
{
    public Guid EmployeeId { get; } = employeeId;
    public string EmployeeFullName { get; } = employeeFullName;
}
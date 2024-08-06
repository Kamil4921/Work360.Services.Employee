using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeCreated(long employeeId) : INotification
{
    public long EmployeeId { get; } = employeeId;
}
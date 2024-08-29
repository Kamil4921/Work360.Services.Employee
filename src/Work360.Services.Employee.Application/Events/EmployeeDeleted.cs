using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeDeleted(Guid employeeId) : INotification
{
    public Guid EmployeeId { get; } = employeeId;
}
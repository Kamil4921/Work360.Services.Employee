using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeStateChanged(Guid employeeId, string previousState, string currentState) : INotification
{
    public Guid EmployeeId { get; } = employeeId;
    public string PreviousState { get; } = previousState;
    public string CurrentState { get; } = currentState;
}
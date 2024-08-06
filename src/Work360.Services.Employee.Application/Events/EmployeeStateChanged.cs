using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeStateChanged(long employeeId, string previousState, string currentState) : INotification
{
    public long Employee { get; } = employeeId;
    public string PreviousState { get; } = previousState;
    public string CurrentState { get; } = currentState;
}
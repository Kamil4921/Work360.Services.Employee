using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeContractTypeChanged(long employeeId, string previousContract, string currentContract) : INotification
{
    public long EmployeeId { get; } = employeeId;
    public string PreviousContract { get; } = previousContract;
    public string CurrentContract { get; } = currentContract;
}
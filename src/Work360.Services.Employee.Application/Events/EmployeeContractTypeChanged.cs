using MediatR;

namespace Work360.Services.Employee.Application.Events;

public class EmployeeContractTypeChanged(Guid employeeId, string previousContract, string currentContract) : INotification
{
    public Guid EmployeeId { get; } = employeeId;
    public string PreviousContract { get; } = previousContract;
    public string CurrentContract { get; } = currentContract;
}
using MediatR;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
internal sealed class ChangeEmployeeState(long employeeId, string state) : IRequest
{
    public long EmployeeId { get; } = employeeId;
    public string State { get; } = state;
}
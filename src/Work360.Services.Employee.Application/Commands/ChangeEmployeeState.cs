using MediatR;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
internal sealed class ChangeEmployeeState(Guid employeeId, string state) : IRequest
{
    public Guid EmployeeId { get; } = employeeId;
    public string State { get; } = state;
}
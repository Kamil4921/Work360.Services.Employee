using MediatR;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
public class ChangeEmployeeContract(Guid employeeId, string contract) : IRequest
{
    public Guid EmployeeId { get; } = employeeId;
    public string Contract { get; } = contract;
}
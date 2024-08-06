using MediatR;

namespace Work360.Services.Employee.Application.Commands;

public class ChangeEmployeeContract(long employeeId, string contract) : IRequest
{
    public long EmployeeId { get; } = employeeId;
    public string Contract { get; } = contract;
}
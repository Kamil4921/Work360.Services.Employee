using MediatR;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
public class DeleteEmployee(Guid employeeId) : IRequest
{
    public Guid EmployeeId { get; } = employeeId;
}
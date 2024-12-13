using MediatR;
using Work360.Services.Employee.Application.DTO;

namespace Work360.Services.Employee.Application.Queries;

[Contract]
public class GetEmployee(Guid employeeId) : IRequest<EmployeeDto>
{
    public Guid EmployeeId { get; } = employeeId;
}
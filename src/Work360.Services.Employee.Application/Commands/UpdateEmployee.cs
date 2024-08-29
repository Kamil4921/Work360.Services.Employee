using MediatR;

namespace Work360.Services.Employee.Application.Commands;

public class UpdateEmployee(Core.Entities.Employee employee) : IRequest
{
    public Core.Entities.Employee Employee { get; } = employee;
}
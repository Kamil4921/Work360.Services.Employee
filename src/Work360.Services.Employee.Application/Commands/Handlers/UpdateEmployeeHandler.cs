using MediatR;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class UpdateEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployee>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task Handle(UpdateEmployee command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployee(command.Id);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(command.Id);
        }

        employee.Pesel = command.Pesel;
        employee.TypeOfContract = command.TypeOfContract;
        employee.State = command.State;
        employee.Address = command.Address ?? employee.Address;
        employee.Email = command.Email ?? employee.Email;
        employee.Position = command.Position ?? employee.Position;
        employee.Salary = command.Salary;
        employee.FullName = command.FullName ?? employee.FullName;
        employee.HiredAt = new DateTime(command.HiredAt.Ticks, DateTimeKind.Utc);

        await _employeeRepository.UpdateEmployee(employee);
    }
}
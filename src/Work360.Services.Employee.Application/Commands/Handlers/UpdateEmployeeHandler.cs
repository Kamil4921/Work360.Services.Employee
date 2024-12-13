using MediatR;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class UpdateEmployeeHandler(IEmployeeRepository employeeRepository,  IEventMapper eventMapper, IMessageBroker messageBroker) : IRequestHandler<UpdateEmployee>
{
    public async Task Handle(UpdateEmployee command, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployee(command.Id);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(command.Id);
        }

        var previousEmployeeData = employee;
        employee.Pesel = command.Pesel;
        employee.TypeOfContract = command.TypeOfContract;
        employee.State = command.State;
        employee.Address = command.Address ?? employee.Address;
        employee.Email = command.Email ?? employee.Email;
        employee.Position = command.Position ?? employee.Position;
        employee.Salary = command.Salary;
        employee.FullName = command.FullName ?? employee.FullName;
        employee.HiredAt = new DateTime(command.HiredAt.Ticks, DateTimeKind.Utc);
        
        await employeeRepository.UpdateEmployee(employee);
        employee.EmployeeUpdated(previousEmployeeData);
        
        var events = eventMapper.MapAll(employee.Events);
        await messageBroker.PublishAsync(events);
    }
}
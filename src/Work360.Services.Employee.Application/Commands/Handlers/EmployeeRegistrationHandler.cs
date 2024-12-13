using MediatR;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class EmployeeRegistrationHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker)
    : IRequestHandler<EmployeeRegistration, Guid>
{
    public async Task<Guid> Handle(EmployeeRegistration command, CancellationToken cancellationToken)
    {
        var utcDateTime = new DateTime(command.HiredAt.Ticks, DateTimeKind.Utc);
        var employee = new Core.Entities.Employee(command.Pesel, command.Email, command.Position, command.Salary,
            utcDateTime, command.FullName, command.Address, command.TypeOfContract, command.State);
        
        await employeeRepository.AddEmployee(employee);
        
        var events = eventMapper.MapAll(employee.Events);
        await messageBroker.PublishAsync(events);
        
        return employee.Id;
    }
}
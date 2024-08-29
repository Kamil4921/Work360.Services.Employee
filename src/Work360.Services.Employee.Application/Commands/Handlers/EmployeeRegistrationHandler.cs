using MediatR;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class EmployeeRegistrationHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker)
    : IRequestHandler<EmployeeRegistration>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IEventMapper _eventMapper = eventMapper;
    private readonly IMessageBroker _messageBroker = messageBroker;

    public async Task Handle(EmployeeRegistration command, CancellationToken cancellationToken)
    {
        var employee = new Core.Entities.Employee(command.Pesel, command.Email, command.Position, command.Salary,
            command.HiredAt, command.FullName, command.Address, command.TypeOfContract, command.State);
        
        await _employeeRepository.AddEmployee(employee);
        
        var events = _eventMapper.MapAll(employee.Events);
        await _messageBroker.PublishAsync(events);
    }
}
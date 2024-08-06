using MediatR;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class CompleteEmployeeRegistrationHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker)
    : IRequestHandler<CompleteEmployeeRegistration>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IEventMapper _eventMapper = eventMapper;
    private readonly IMessageBroker _messageBroker = messageBroker;

    public async Task Handle(CompleteEmployeeRegistration command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployee(command.EmployeeId);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }
        
        employee.CompleteRecruitment(command.FullName, command.Position, command.HiredAt, command.Address);
        await _employeeRepository.UpdateEmployee(employee);
        
        var events = _eventMapper.MapAll(employee.Events);
        await _messageBroker.PublishAsync(events);
    }
}
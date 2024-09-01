using MediatR;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker) : IRequestHandler<DeleteEmployee>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IEventMapper _eventMapper = eventMapper;
    private readonly IMessageBroker _messageBroker = messageBroker;

    public async Task Handle(DeleteEmployee command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployee(command.EmployeeId);
        await _employeeRepository.DeleteEmployee(command.EmployeeId);
        
        employee.EmployeeDeleted();
        var events = _eventMapper.MapAll(employee.Events);
        await _messageBroker.PublishAsync(events);
    }
}
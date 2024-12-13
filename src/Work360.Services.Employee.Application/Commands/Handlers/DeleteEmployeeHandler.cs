using MediatR;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker) : IRequestHandler<DeleteEmployee>
{
    public async Task Handle(DeleteEmployee command, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployee(command.EmployeeId);
        await employeeRepository.DeleteEmployee(command.EmployeeId);
        
        employee.EmployeeDeleted();
        var events = eventMapper.MapAll(employee.Events);
        await messageBroker.PublishAsync(events);
    }
}
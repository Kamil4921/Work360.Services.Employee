using MediatR;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class ChangeEmployeeStateHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker)
    : IRequestHandler<ChangeEmployeeState>
{
    private readonly IEventMapper _eventMapper = eventMapper;
    private readonly IMessageBroker _messageBroker = messageBroker;

    public async Task Handle(ChangeEmployeeState command, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployee(command.EmployeeId) ?? throw new EmployeeNotFoundException(command.EmployeeId);

        if(!Enum.TryParse<State>(command.State, true, out var state)){
            throw new CannotChangeEmployeeStateException(command.EmployeeId, command.State);
        }

        if(employee.State == state){
            return;
        }

        switch (state){
            case State.Hired:
                employee.Hired();
                break;
            case State.InRecruitment:
                employee.InRecruitment();
                break;
            case State.Left:
                employee.Left();
                break;
            case State.Fired:
                employee.Fired();
                break;
            default:
                throw new CannotChangeEmployeeStateException(command.EmployeeId, command.State);            
        }

        await employeeRepository.UpdateEmployee(employee);
        var events = _eventMapper.MapAll(employee.Events);
        await _messageBroker.PublishAsync(events);
    }
}
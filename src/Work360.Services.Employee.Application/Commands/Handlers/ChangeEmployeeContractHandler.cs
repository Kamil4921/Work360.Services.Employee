using MediatR;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class ChangeEmployeeContractHandler(IEmployeeRepository employeeRepository, IEventMapper eventMapper, IMessageBroker messageBroker)
    : IRequestHandler<ChangeEmployeeContract>
{
    public async Task Handle(ChangeEmployeeContract command, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployee(command.EmployeeId);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }

        if (!Enum.TryParse<Contract>(command.Contract, true, out var contract))
        {
            throw new CannotChangeEmployeeContractException(command.EmployeeId, command.Contract);
        }

        if (employee.TypeOfContract == contract)
        {
            return;
        }

        switch (contract)
        {
            case Contract.Employment:
                employee.EmploymentContract();
                break;
            case Contract.Performance:
                employee.PerformanceContract();
                break;
            case Contract.B2B:
                employee.B2BContract();
                break;
            case Contract.Order:
                employee.OrderContract();
                break;
            default:
                throw new CannotChangeEmployeeContractException(command.EmployeeId, command.Contract);     
        }

        await employeeRepository.UpdateEmployee(employee);
        var events = eventMapper.MapAll(employee.Events);
        await messageBroker.PublishAsync(events);
    }
}
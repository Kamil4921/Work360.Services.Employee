using System.Diagnostics.CodeAnalysis;
using MediatR;
using Work360.Services.Employee.Application.Commands;
using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application;

sealed internal class ChangeEmployeeStateHandler : IRequestHandler<ChangeCustomerState>
{
    private readonly IEmployeeRepository _employeeRepository;

    public ChangeEmployeeStateHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(ChangeCustomerState command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployee(command.Id) ?? throw new EmployeeNotFoundException(command.Id);

        if(!Enum.TryParse<State>(command.State, true, out var state)){
            throw new CannotChangeEmployeeStateException(command.Id, command.State);
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
                throw new CannotChangeEmployeeStateException(command.Id, command.State);            
        }

        await _employeeRepository.UpdateEmployee(employee);
        //TODO Add event
    }
}
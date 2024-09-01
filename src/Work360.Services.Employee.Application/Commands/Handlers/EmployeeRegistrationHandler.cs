using MediatR;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

public class EmployeeRegistrationHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<EmployeeRegistration, Guid>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Guid> Handle(EmployeeRegistration command, CancellationToken cancellationToken)
    {
        var utcDateTime = new DateTime(command.HiredAt.Ticks, DateTimeKind.Utc);
        var employee = new Core.Entities.Employee(command.Pesel, command.Email, command.Position, command.Salary,
            utcDateTime, command.FullName, command.Address, command.TypeOfContract, command.State);
        
        await _employeeRepository.AddEmployee(employee);
        
        //var events = _eventMapper.MapAll(employee.Events);
        //await _messageBroker.PublishAsync(events);
        
        return employee.Id;
    }
}
using MediatR;
using Work360.Services.Employee.Application.DTO;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Queries.Handlers;

internal sealed class GetEmployeeHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<GetEmployee, EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<EmployeeDto> Handle(GetEmployee request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployee(request.EmployeeId);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(request.EmployeeId);
        }
        
        var employeeDto = new EmployeeDto
        {
            State = employee.State,
            Address = employee.Address,
            Email = employee.Email,
            Position = employee.Position,
            Salary = employee.Salary,
            CreatedAt = employee.CreatedAt,
            FullName = employee.FullName,
            HiredAt = employee.HiredAt,
            TypeOfContract = employee.TypeOfContract
        };

        return employeeDto;
    }
}
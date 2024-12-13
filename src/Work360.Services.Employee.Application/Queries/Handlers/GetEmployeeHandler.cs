using MediatR;
using Work360.Services.Employee.Application.DTO;
using Work360.Services.Employee.Application.Exceptions;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Queries.Handlers;

internal sealed class GetEmployeeHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<GetEmployee, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(GetEmployee request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployee(request.EmployeeId);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(request.EmployeeId);
        }
        
        var employeeDto = new EmployeeDto
        {
            Pesel = employee.Pesel,
            State = employee.State.ToString(),
            Address = employee.Address,
            Email = employee.Email,
            Position = employee.Position,
            Salary = employee.Salary,
            CreatedAt = employee.CreatedAt,
            FullName = employee.FullName,
            HiredAt = employee.HiredAt,
            TypeOfContract = employee.TypeOfContract.ToString()
        };

        return employeeDto;
    }
}
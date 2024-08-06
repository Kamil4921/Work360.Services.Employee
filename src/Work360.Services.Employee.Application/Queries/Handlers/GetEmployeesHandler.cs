using MediatR;
using Work360.Services.Employee.Application.DTO;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Queries.Handlers;

internal sealed class GetEmployeesHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<GetEmployees, IEnumerable<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployees request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetEmployees();

        var employeesDto = employees.Select(employee => new EmployeeDto
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
            })
            .ToList();

        return employeesDto;
    }
}
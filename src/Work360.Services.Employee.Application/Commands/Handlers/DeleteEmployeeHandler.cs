using MediatR;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class DeleteEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<DeleteEmployee>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task Handle(DeleteEmployee request, CancellationToken cancellationToken)
    {
        await _employeeRepository.DeleteEmployee(request.EmployeeId);
    }
}
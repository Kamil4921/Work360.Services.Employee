using MediatR;
using Work360.Services.Employee.Core.Repositories;

namespace Work360.Services.Employee.Application.Commands.Handlers;

internal sealed class UpdateEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployee>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task Handle(UpdateEmployee request, CancellationToken cancellationToken)
    {
        await _employeeRepository.UpdateEmployee(request.Employee);
    }
}
using MediatR;
using Work360.Services.Employee.Application.DTO;

namespace Work360.Services.Employee.Application.Queries;

public class GetEmployee : IRequest<EmployeeDto>
{
    public long EmployeeId { get; set; }
}
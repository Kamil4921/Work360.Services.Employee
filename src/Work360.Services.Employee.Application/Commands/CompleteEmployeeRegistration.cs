using MediatR;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
public sealed class CompleteEmployeeRegistration(string fullName, string position, DateTime hiredAt, string address, long employeeId)
    : IRequest
{
    public long EmployeeId { get; } = employeeId;
    public string FullName { get; } = fullName;
    public string Position { get; } = position;
    public DateTime HiredAt { get; } = hiredAt;
    public string Address { get; } = address;
}
using MediatR;
using Work360.Services.Employee.Core.Entities;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
public class EmployeeRegistration(long pesel, string email, int salary, Contract typeOfContract, State state, string fullName, string position, DateTime hiredAt, string address) : IRequest<Guid>
{
    public long Pesel { get; } = pesel;
    public string Email { get; } = email;
    public int Salary { get; } = salary;
    public Contract TypeOfContract { get; } = typeOfContract;
    public State State { get; } = state;
    public string FullName { get; } = fullName;
    public string Position { get; } = position;
    public DateTime HiredAt { get; } = hiredAt;
    public string Address { get; } = address;
}
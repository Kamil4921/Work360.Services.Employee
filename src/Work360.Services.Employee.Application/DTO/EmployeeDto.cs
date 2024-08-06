using Work360.Services.Employee.Core.Entities;

namespace Work360.Services.Employee.Application.DTO;

public class EmployeeDto
{
    public string Email { get; set; }
    public string Position { get; set; }
    public int Salary { get; set; }
    public DateTime HiredAt { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public Contract TypeOfContract { get; set; }
    public State State { get; set; }
    public DateTime CreatedAt { get; set; }
}
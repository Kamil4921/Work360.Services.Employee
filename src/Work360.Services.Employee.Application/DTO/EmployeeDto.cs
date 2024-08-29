namespace Work360.Services.Employee.Application.DTO;

public class EmployeeDto
{
    public long Pesel { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public int Salary { get; set; }
    public DateTime HiredAt { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string TypeOfContract { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
}
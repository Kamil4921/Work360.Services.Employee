namespace Work360.Services.Employee.Core.Events;

public class EmployeeRecruitmentCompleted(Entities.Employee employee) : IDomainEvent
{
    public Entities.Employee Employee { get; } = employee;
}
namespace Work360.Services.Employee.Core.Exceptions;

public class InvalidEmployeeFullNameException(long id, string fullName)
    : DomainException($"Employee with Id: {id} has invalid full name: {fullName}")
{
    public override string Code => "invalid_employee_fullname";
    public long Id { get; } = id;
    public string FullName { get; } = fullName;
}
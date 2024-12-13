namespace Work360.Services.Employee.Core.Exceptions;

public class InvalidEmployeeHiredDateException(long id, DateTime hiredDate)
    : DomainException($"Employee with Id: {id} has invalid hired date: {hiredDate}")
{
    public override string Code => "invalid_employee_hiredDate";
    public long Id { get; } = id;
    public DateTime HiredDate { get; } = hiredDate;
}
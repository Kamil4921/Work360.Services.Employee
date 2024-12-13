namespace Work360.Services.Employee.Core.Exceptions;

public class InvalidEmployeeAddressException(long id, string address)
    : DomainException($"Employee with Id: {id} has invalid address: {address}")
{
    public override string Code => "invalid_employee_address";
    public long Id { get; } = id;
    public string Address { get; } = address;
}

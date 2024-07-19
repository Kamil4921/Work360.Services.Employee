namespace Work360.Services.Employee.Core.Exceptions
{
    public class InvalidEmployeePositionException(long id, string position) : DomainException($"Employee with Id: {id} has invalid position: {position}")
    {
        public override string Code { get; } = "invalid_employee_position";
        public long Id { get; } = id;
        public string Position { get; } = position;
    }
}
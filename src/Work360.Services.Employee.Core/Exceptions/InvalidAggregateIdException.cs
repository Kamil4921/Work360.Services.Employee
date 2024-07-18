namespace Work360.Services.Employee.Core.Exceptions;

public class InvalidAggregateIdException() : DomainException($"Invalid aggregate id.")
{
    public override string Code { get; } = "invalid_aggregate_id";
}
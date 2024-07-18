namespace Work360.Services.Employee.Core.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{
    public virtual string Code { get; }
}
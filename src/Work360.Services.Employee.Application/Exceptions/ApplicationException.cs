namespace Work360.Services.Employee.Application.Exceptions;

public abstract class ApplicationException(string message) : Exception(message)
{
    public virtual string Code { get; }
}
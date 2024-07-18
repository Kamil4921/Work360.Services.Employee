namespace Work360.Services.Employee.Core.Entities;

public class Employee : AggregateRoot
{
    private ISet<Guid> _completedOrders = new HashSet<Guid>();
    
    public string Email { get; private set; }
    public string Position { get; private set; }
    public int Salary { get; private set; }
    public DateTime HiredAt { get; private set; }
    public string FullName { get; private set; }
    public string Address { get; private set; }
    public Contract TypeOfContract { get; set; }
    public State State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    
}
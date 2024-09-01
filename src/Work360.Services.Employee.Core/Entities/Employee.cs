using Work360.Services.Employee.Core.Events;
using Work360.Services.Employee.Core.Exceptions;

namespace Work360.Services.Employee.Core.Entities;

public class Employee : AggregateRoot
{
    public long Pesel { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public int Salary { get; set; }
    public DateTime HiredAt { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public Contract TypeOfContract { get; set; }
    public State State { get; set; }
    public DateTime CreatedAt { get; private set; }
    
    public Employee(long pesel, string email, string position) : this(pesel,email, position, 0, DateTime.Now, string.Empty, string.Empty, Contract.Unknown, State.Unknown)
    {
    }

    public Employee(long pesel, string email, string position, int salary, DateTime hiredAt, string fullName, string address, Contract typeOfContract, State state){

        Id = Guid.NewGuid();
        Pesel = pesel;
        Email = email;
        Position = string.IsNullOrWhiteSpace(position) ? throw new InvalidEmployeePositionException(Pesel, position) : position;
        Salary = salary;
        HiredAt = hiredAt.Date > DateTime.Today.Date ? throw new InvalidEmployeeHiredDateException(Pesel, hiredAt) : hiredAt;
        FullName = fullName;
        Address = address;
        TypeOfContract = typeOfContract;
        State = state;
        CreatedAt = DateTime.UtcNow;
        Version = 1;
        AddEvent(new EmployeeRecruitmentCompleted(this));
    }

    public void Hired() => SetState(State.Hired);
    public void Fired() => SetState(State.Fired);
    public void InRecruitment() => SetState(State.InRecruitment);
    public void Left() => SetState(State.Left);
    
    public void EmploymentContract() => SetTypeOfContract(Contract.Employment);
    public void PerformanceContract() => SetTypeOfContract(Contract.Performance);
    public void OrderContract() => SetTypeOfContract(Contract.Order);
    public void B2BContract() => SetTypeOfContract(Contract.B2B);

    public void EmployeeDeleted() => AddEvent(new EmployeeDeleted(this));

    public void EmployeeUpdated(Employee previousEmployeeData) =>
        AddEvent(new EmployeeUpdated(this, previousEmployeeData));
    
    private void SetState(State state){
        var previousState = State;
        State = state;
        AddEvent(new EmployeeStateChanged(this, previousState));
    }

    private void SetTypeOfContract(Contract contract){
        var previousContract = contract;
        TypeOfContract = contract;
        AddEvent(new EmployeeContractTypeChanged(this, previousContract));
    }
}
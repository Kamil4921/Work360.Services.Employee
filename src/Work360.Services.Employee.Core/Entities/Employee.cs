using Work360.Services.Employee.Core.Events;
using Work360.Services.Employee.Core.Exceptions;

namespace Work360.Services.Employee.Core.Entities;

public class Employee : AggregateRoot
{   
    public string Email { get; private set; }
    public string Position { get; private set; }
    public int Salary { get; private set; }
    public DateTime HiredAt { get; private set; }
    public string FullName { get; private set; }
    public string Address { get; private set; }
    public Contract TypeOfContract { get; set; }
    public State State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Employee(long id, string email, string position, DateTime createdAt) : this(new AggregateId(id), email, position, 0, DateTime.Now, string.Empty, string.Empty, Contract.Unknown, State.Unknown, createdAt)
    {
    }

    public Employee(AggregateId id, string email, string position, int salary, DateTime hiredAt, string fullName, string address, Contract typeOfContract, State state, DateTime createdAt ){

        Id = id;
        Email = email;
        Position = position;
        Salary = salary;
        HiredAt = hiredAt;
        FullName = fullName;
        Address = address;
        TypeOfContract = typeOfContract;
        State = state;
        CreatedAt = createdAt;
    }

    public void Hired() => SetState(State.Hired);
    public void Fired() => SetState(State.Fired);
    public void InRecruitment() => SetState(State.InRecruitment);
    public void Left() => SetState(State.Left);

    public void CompleteRecruitment(string fullName, string position, DateTime hiredAt, string address){

        if(string.IsNullOrWhiteSpace(fullName)){
            throw new InvalidEmployeeFullNameException(Id.PESEL, fullName);
        }

        if(string.IsNullOrWhiteSpace(position)){
            throw new InvalidEmployeePositionException(Id.PESEL, position);
        }

        if(hiredAt > DateTime.Today){
            throw new InvalidEmployeeHiredDateException(Id.PESEL, hiredAt);
        }

        if(string.IsNullOrWhiteSpace(address)){
            throw new InvalidEmployeeAddressException(Id.PESEL, address);
        }

        FullName = fullName;
        Position = position;
        HiredAt = hiredAt;
        Address = address;
        AddEvent(new EmployeeRecruitmentCompleted(this));
    }

    private void SetState(State state){
        var previousState = State;
        State = state;
        AddEvent(new EmployeeStateChanged(this, previousState));
    }

    public void EmploymentContract() => SetTypeOfContract(Contract.Employment);
    public void PerformanceContract() => SetTypeOfContract(Contract.Performance);
    public void OrderContract() => SetTypeOfContract(Contract.Order);
    public void B2BContract() => SetTypeOfContract(Contract.B2B);

    private void SetTypeOfContract(Contract contract){
        var previousContract = contract;
        TypeOfContract = contract;
        AddEvent(new EmployeeContractTypeChanged(this, previousContract));
    }
}
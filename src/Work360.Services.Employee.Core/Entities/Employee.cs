using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Work360.Services.Employee.Core.Events;
using Work360.Services.Employee.Core.Exceptions;

namespace Work360.Services.Employee.Core.Entities;

public class Employee : AggregateRoot
{
    public long Pesel { get; private set; }
    public string Email { get; private set; }
    public string Position { get; private set; }
    public int Salary { get; private set; }
    public DateTime HiredAt { get; private set; }
    public string FullName { get; private set; }
    public string Address { get; set; }
    public Contract TypeOfContract { get; set; }
    public State State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Employee(long pesel, string email, string position, DateTime createdAt) : this(pesel ,email, position, 0, DateTime.Now, string.Empty, string.Empty, Contract.Unknown, State.Unknown, createdAt)
    {
    }

    public Employee(long pesel ,string email, string position, int salary, DateTime hiredAt, string fullName, string address, Contract typeOfContract, State state, DateTime createdAt ){

        Id = Guid.NewGuid();
        Pesel = pesel;
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
            throw new InvalidEmployeeFullNameException(Pesel, fullName);
        }

        if(string.IsNullOrWhiteSpace(position)){
            throw new InvalidEmployeePositionException(Pesel, position);
        }

        if(hiredAt > DateTime.Today){
            throw new InvalidEmployeeHiredDateException(Pesel, hiredAt);
        }

        if(string.IsNullOrWhiteSpace(address)){
            throw new InvalidEmployeeAddressException(Pesel, address);
        }

        FullName = fullName;
        Position = position;
        HiredAt = hiredAt;
        Address = address;
        AddEvent(new EmployeeRecruitmentCompleted(this));
    }
    
    public void EmploymentContract() => SetTypeOfContract(Contract.Employment);
    public void PerformanceContract() => SetTypeOfContract(Contract.Performance);
    public void OrderContract() => SetTypeOfContract(Contract.Order);
    public void B2BContract() => SetTypeOfContract(Contract.B2B);

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
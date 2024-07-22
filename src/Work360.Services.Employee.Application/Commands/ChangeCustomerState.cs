using MediatR;

namespace Work360.Services.Employee.Application.Commands;

[Contract]
sealed internal class ChangeCustomerState : IRequest
{
    public long Id { get; }
    public string State { get; }

    public ChangeCustomerState(long id, string state)
    {
        Id = id;
        State = state;
    }
}
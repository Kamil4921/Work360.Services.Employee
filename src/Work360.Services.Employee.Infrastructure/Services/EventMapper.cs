using MediatR;
using Work360.Services.Employee.Application.Events;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core;
using Work360.Services.Employee.Core.Events;
using EmployeeContractTypeChanged = Work360.Services.Employee.Core.Events.EmployeeContractTypeChanged;
using EmployeeStateChanged = Work360.Services.Employee.Core.Events.EmployeeStateChanged;

namespace Work360.Services.Employee.Infrastructure.Services;

public class EventMapper : IEventMapper
{
    public INotification? Map(IDomainEvent @event)
    {
        return @event switch
        {
            EmployeeRecruitmentCompleted e => new EmployeeCreated(e.Employee.Pesel),
            EmployeeContractTypeChanged e => new Application.Events.EmployeeContractTypeChanged(e.Employee.Pesel,
                e.PreviousContract.ToString(), e.Employee.TypeOfContract.ToString()),
            EmployeeStateChanged e => new Application.Events.EmployeeStateChanged(e.Employee.Pesel,
                e.PreviousState.ToString(), e.Employee.State.ToString()),
            _ => null
        };
    }

    public IEnumerable<INotification?> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
}
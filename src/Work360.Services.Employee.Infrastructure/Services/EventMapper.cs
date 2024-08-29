using MediatR;
using Work360.Services.Employee.Application.Events;
using Work360.Services.Employee.Application.Services;
using Work360.Services.Employee.Core;
using Work360.Services.Employee.Core.Events;
using EmployeeContractTypeChanged = Work360.Services.Employee.Core.Events.EmployeeContractTypeChanged;
using EmployeeDeleted = Work360.Services.Employee.Core.Events.EmployeeDeleted;
using EmployeeStateChanged = Work360.Services.Employee.Core.Events.EmployeeStateChanged;
using EmployeeUpdated = Work360.Services.Employee.Core.Events.EmployeeUpdated;

namespace Work360.Services.Employee.Infrastructure.Services;

public class EventMapper : IEventMapper
{
    public INotification? Map(IDomainEvent @event)
    {
        return @event switch
        {
            EmployeeRecruitmentCompleted e => new EmployeeCreated(e.Employee.Id),
            EmployeeContractTypeChanged e => new Application.Events.EmployeeContractTypeChanged(e.Employee.Id,
                e.PreviousContract.ToString(), e.Employee.TypeOfContract.ToString()),
            EmployeeStateChanged e => new Application.Events.EmployeeStateChanged(e.Employee.Id,
                e.PreviousState.ToString(), e.Employee.State.ToString()),
            EmployeeDeleted e => new Application.Events.EmployeeDeleted(e.Employee.Id),
            EmployeeUpdated e => new Application.Events.EmployeeUpdated(e.Employee, e.PreviousEmployee),
            _ => null
        };
    }

    public IEnumerable<INotification?> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
}
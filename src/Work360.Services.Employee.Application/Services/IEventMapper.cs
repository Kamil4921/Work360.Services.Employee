using MediatR;
using Work360.Services.Employee.Core;

namespace Work360.Services.Employee.Application.Services;

public interface IEventMapper
{
    INotification Map(IDomainEvent @event);
    IEnumerable<INotification?> MapAll(IEnumerable<IDomainEvent> events);
}
using MediatR;

namespace Work360.Services.Employee.Application.Services;

public interface IMessageBroker
{
    Task PublishAsync(params INotification[] events);
    Task PublishAsync(IEnumerable<INotification> events);
}
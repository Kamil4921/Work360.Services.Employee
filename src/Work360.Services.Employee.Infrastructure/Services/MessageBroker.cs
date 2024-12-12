using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;
using Work360.Services.Employee.Application.Events;
using Work360.Services.Employee.Application.Services;

namespace Work360.Services.Employee.Infrastructure.Services;

public class MessageBroker : IMessageBroker
{
    private const string ConnectionString = "Endpoint=sb://localhost:5672/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=RootManageSharedAccessKeyValue;UseDevelopmentEmulator=true;";
    public Task PublishAsync(params INotification[] events) => PublishAsync(events.AsEnumerable());

    public async Task PublishAsync(IEnumerable<INotification> events)
    {
        var client = new ServiceBusClient(ConnectionString);
        var sender = client.CreateSender("employee-topic");
        var message = new ServiceBusMessage(Newtonsoft.Json.JsonConvert.SerializeObject(events));
        
        foreach (var @event in events)
        {
            await sender.SendMessageAsync(message);
        }
    }
}
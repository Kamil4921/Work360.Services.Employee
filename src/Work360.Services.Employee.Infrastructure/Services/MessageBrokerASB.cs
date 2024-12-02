using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Azure.Amqp.Serialization;
using Work360.Services.Employee.Application.Services;

namespace Work360.Services.Employee.Infrastructure.Services;

public class MessageBrokerASB : IMessageBroker
{
    private const string connectionString = "Endpoint=sb://localhost:5672/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=RootManageSharedAccessKeyValue;UseDevelopmentEmulator=true;";
    public async Task PublishAsync(params INotification[] events)
    {
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender("employee-topic");
        var message = new ServiceBusMessage(JsonSerializer.Serialize(events, new JsonSerializerOptions{ WriteIndented = true }));

        await sender.SendMessageAsync(message);
    }

    public async Task PublishAsync(IEnumerable<INotification> events)
    {
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender("employee-topic");

        foreach (var @event in events)
        {
            await PublishAsync(@event);
        }
        //var messages = events.Select(@event => new ServiceBusMessage(JsonSerializer.Serialize(@event)));

        //await sender.SendMessagesAsync(messages);
    }
}
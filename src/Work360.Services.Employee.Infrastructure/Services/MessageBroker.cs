using System.Text;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Work360.Services.Employee.Application.Services;

namespace Work360.Services.Employee.Infrastructure.Services;

public class MessageBroker : IMessageBroker
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBroker()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange:"trigger", type: ExchangeType.Fanout);
    }

    public Task PublishAsync(params INotification[] events) => PublishAsync(events?.AsEnumerable());

    public Task PublishAsync(IEnumerable<INotification> events)
    {
        //logging
        if (_connection.IsOpen)
        {
            foreach (var @event in events)
            {
                //logging
                
                var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, Formatting.Indented));
                _channel.BasicPublish(exchange: "trigger",
                                    routingKey:"",
                                    basicProperties: null,
                                    body: message);
                
            }
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
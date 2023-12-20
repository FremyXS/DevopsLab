using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Laborotor.Services.Rabbit
{
    public class RabbitMqService : IRabbitMqService
    {
        public RabbitMqService() { 
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "laborotor-rabbitmq",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("links", durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "links", body: body);
        }
    }
}

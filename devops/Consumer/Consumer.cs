using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Consumer.Models;

namespace Consumer
{
    public class Consumer : BackgroundService
    {
        private IServiceProvider _sp;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        public Consumer(IServiceProvider sp)
        {
            _sp = sp;

            _factory = new ConnectionFactory() { HostName = "laborotor-rabbitmq" };

            _connection = _factory.CreateConnection();  

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "links",
                durable: true,
                exclusive: false);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();

                var link = JsonSerializer.Deserialize<Link>(Encoding.UTF8.GetString(body));

                Console.WriteLine(link);

                Task.Run(async () =>
                {
                    using (var scope = _sp.CreateScope())
                    {
                        var httpService = scope.ServiceProvider.GetRequiredService<IHttpService>();

                        var code = await httpService.Send(link.Url);

                        await httpService.UpdateLink(new StatusUpdateRequest(link.Id, true, code));


                        Console.WriteLine("=================================================================");

                        Console.WriteLine($"Status of link with {link.Id} has been changed on {link.Status}");

                        Console.WriteLine("=================================================================");
                    }
                });

            };

            _channel.BasicConsume(queue: "links", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}

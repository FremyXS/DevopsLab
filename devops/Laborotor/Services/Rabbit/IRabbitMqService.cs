namespace Laborotor.Services.Rabbit
{
    public interface IRabbitMqService
    {
        void SendMessage<T>(T obj);
    }
}

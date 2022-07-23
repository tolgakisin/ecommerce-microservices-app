using RabbitMQ.Client;

namespace EventBus.RabbitMQ.RabbitMQ
{
    public interface IRabbitMQBase
    {
        IConnection CreateBus();
    }
}
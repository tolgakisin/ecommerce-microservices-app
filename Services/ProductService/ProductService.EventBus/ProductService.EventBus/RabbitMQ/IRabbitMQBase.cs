using RabbitMQ.Client;

namespace ProductService.EventBus.RabbitMQ
{
    public interface IRabbitMQBase
    {
        IConnection CreateConnection();
    }
}
using RabbitMQ.Client;

namespace BasketService.EventBus.RabbitMQ
{
    public interface IRabbitMQBase
    {
        IConnection CreateConnection();
    }
}
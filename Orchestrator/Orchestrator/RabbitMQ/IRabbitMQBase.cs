using RabbitMQ.Client;

namespace Orchestrator.RabbitMQ
{
    public interface IRabbitMQBase
    {
        IConnection CreateBus();
    }
}

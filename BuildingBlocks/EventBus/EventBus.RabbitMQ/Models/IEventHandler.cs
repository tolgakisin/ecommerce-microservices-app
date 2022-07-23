using System.Threading.Tasks;

namespace EventBus.RabbitMQ.Models
{
    public interface IEventHandler<TEvent> : IEventHandler where TEvent : IEvent
    {
        Task<TEvent> Handle(TEvent @event);
        Task<TEvent> HandleReverse(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}

using System.Threading.Tasks;

namespace BasketService.EventBus.Models
{
    public interface IEventHandler<TEvent> : IEventHandler where TEvent : IEvent
    {
        Task Handle(TEvent @event);
        Task HandleReverse(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}

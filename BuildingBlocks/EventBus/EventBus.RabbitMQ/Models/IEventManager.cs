namespace EventBus.RabbitMQ.Models
{
    public interface IEventManager
    {
        void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
        void UnSubscribe<TEvent>() where TEvent : IEvent;
        IEvent Publish(IEvent @event, string publishEventName = null);
    }
}

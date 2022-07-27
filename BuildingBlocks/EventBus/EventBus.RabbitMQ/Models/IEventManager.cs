namespace EventBus.RabbitMQ.Models
{
    public interface IEventManager
    {
        void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
        void UnSubscribe<TEvent>() where TEvent : IEvent;
        TEvent Publish<TEvent>(TEvent @event, bool isOrchestration = true) where TEvent : IEvent;
    }
}

using EventBus.Core;
using EventBus.RabbitMQ.Models;
using OrderService.API.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace OrderService.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IEventManager _eventManager;

        public OrderCreatedEventHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Task<OrderCreatedEvent> Handle(OrderCreatedEvent @event)
        {
            // TODO: Create Order

            Guid orderId = Guid.NewGuid();

            _eventManager.Publish(new OrderStartedEvent(@event.UserId, orderId), EventNames.OrchestratorGeneralEvent);

            return Task.FromResult(@event);
        }

        public Task<OrderCreatedEvent> HandleReverse(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

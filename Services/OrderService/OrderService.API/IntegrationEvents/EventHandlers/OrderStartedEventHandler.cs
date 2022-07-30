using EventBus.Core;
using EventBus.RabbitMQ.Models;
using OrderService.API.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace OrderService.API.IntegrationEvents.EventHandlers
{
    public class OrderStartedEventHandler : IEventHandler<OrderStartedEvent>
    {
        private readonly IEventManager _eventManager;

        public OrderStartedEventHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Task Handle(OrderStartedEvent @event)
        {
            // TODO: Create Order

            return Task.CompletedTask;
        }

        public Task HandleReverse(OrderStartedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

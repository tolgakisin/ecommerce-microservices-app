using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using PaymentService.API.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.EventHandlers
{
    [Event(EventNames.BasketService.OrderCreatedEvent)]
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        public Task<OrderCreatedEvent> Handle(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task<OrderCreatedEvent> HandleReverse(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

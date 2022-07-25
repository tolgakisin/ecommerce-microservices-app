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
    [Event(EventNames.OrderCreatedEvent)]
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IEventManager _eventManager;

        public OrderCreatedEventHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Task<OrderCreatedEvent> Handle(OrderCreatedEvent @event)
        {
            // Check Payment process
            bool paymentSuccess = true;

            if (paymentSuccess)
            {
                _eventManager.Publish(new OrderStartedEvent(@event.UserId, @event.CustomerBasket, @event.CustomerAddress, @event.CustomerPayment), EventNames.OrchestratorGeneralEvent);
            }
            else
                _eventManager.Publish(new PaymentFailedEvent(@event.UserId, "Payment error has occured."), EventNames.OrchestratorGeneralEvent);

            return Task.FromResult(@event);
        }

        public Task<OrderCreatedEvent> HandleReverse(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

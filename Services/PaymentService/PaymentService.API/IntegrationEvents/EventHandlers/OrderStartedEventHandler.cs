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
    public class OrderStartedEventHandler : IEventHandler<OrderStartedEvent>
    {
        private readonly IEventManager _eventManager;

        public OrderStartedEventHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Task Handle(OrderStartedEvent @event)
        {
            // Check Payment process
            bool paymentSuccess = true;

            if (paymentSuccess)
            {
                _eventManager.Publish(new PaymentSuccessEvent(@event.UserId, @event.OrderId));
            }
            else
                _eventManager.Publish(new PaymentFailedEvent(@event.UserId, @event.OrderId, $"Payment error has occured. OrderId: {@event.OrderId}"));

            return Task.CompletedTask;
        }

        public Task HandleReverse(OrderStartedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

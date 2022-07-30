using EventBus.RabbitMQ.Models;
using PaymentService.API.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IEventManager _eventManager;

        public OrderCreatedEventHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Task Handle(OrderCreatedEvent @event)
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

        public Task HandleReverse(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

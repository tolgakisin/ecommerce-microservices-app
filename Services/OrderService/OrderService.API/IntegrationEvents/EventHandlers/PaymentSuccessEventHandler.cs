using EventBus.RabbitMQ.Models;
using OrderService.API.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace OrderService.API.IntegrationEvents.EventHandlers
{
    public class PaymentSuccessEventHandler : IEventHandler<PaymentSuccessEvent>
    {
        public Task<PaymentSuccessEvent> Handle(PaymentSuccessEvent @event)
        {
            //TODO: Change status of order on DB

            return Task.FromResult(@event);
        }

        public Task<PaymentSuccessEvent> HandleReverse(PaymentSuccessEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

using EventBus.RabbitMQ.Models;
using OrderService.API.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace OrderService.API.IntegrationEvents.EventHandlers
{
    public class PaymentSuccessEventHandler : IEventHandler<PaymentSuccessEvent>
    {
        public Task Handle(PaymentSuccessEvent @event)
        {
            //TODO: Change status of order on DB

            return Task.CompletedTask;
        }

        public Task HandleReverse(PaymentSuccessEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

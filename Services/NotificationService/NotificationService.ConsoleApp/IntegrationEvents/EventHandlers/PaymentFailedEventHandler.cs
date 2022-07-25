using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    [Event(EventNames.PaymentFailedEvent)]
    public class PaymentFailedEventHandler : IEventHandler<PaymentFailedEvent>
    {
        public Task<PaymentFailedEvent> Handle(PaymentFailedEvent @event)
        {
            Console.WriteLine(@event.ValidationMessage);

            return Task.FromResult(@event);
        }

        public Task<PaymentFailedEvent> HandleReverse(PaymentFailedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

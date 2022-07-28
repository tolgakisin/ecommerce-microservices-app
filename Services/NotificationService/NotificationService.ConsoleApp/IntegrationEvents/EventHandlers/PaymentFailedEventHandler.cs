using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    [Event(EventNames.Payment.PaymentFailedEvent)]
    public class PaymentFailedEventHandler : IEventHandler<PaymentFailedEvent>
    {
        public Task Handle(PaymentFailedEvent @event)
        {
            Console.WriteLine($"Payment Failed. UserId:{@event.UserId} OrderId:{@event.OrderId} Message:{@event.ValidationMessage}");

            return Task.CompletedTask;
        }

        public Task HandleReverse(PaymentFailedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
